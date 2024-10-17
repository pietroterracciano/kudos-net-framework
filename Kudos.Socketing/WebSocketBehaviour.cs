using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Kudos.Socketing.Builders;
using Kudos.Socketing.Descriptors;
using Kudos.Socketing.Interfaces;
using Kudos.Socketing.Packets;
using Kudos.Types.TimeStamps.UnixTimeStamp;
using Kudos.Utils;
using Kudos.Utils.Texts;

namespace Kudos.Socketing;

public sealed class WebSocketBehaviour
    : IStartableWebSocketBehaviour, IStoppableWebSocketBehaviour, IActionableWebSocketBehaviour
{
    #region ... static ...

    private static readonly Byte[]
        __baPing;

    private static readonly String
        __sNormalClosure,
        __sEndpointUnavailable;

    static WebSocketBehaviour()
    {
        __baPing = BytesUtils.Parse("PING");
        __sNormalClosure = nameof(WebSocketCloseStatus.NormalClosure);
        __sEndpointUnavailable = nameof(WebSocketCloseStatus.EndpointUnavailable);
    }

    public static WebSocketBehaviourBuilder RequestBuilder()
    {
        return new WebSocketBehaviourBuilder();
    }

    #endregion

    private readonly WebSocketBehaviourDescriptor _wsbd;
    private readonly Object _lck;
    private Task? _tPingPong;
    private long _iPong;

    internal WebSocketBehaviour(WebSocketBehaviourDescriptor wsbd)
    {
        _wsbd = wsbd;
        _lck = new object();
    }

    #region public ... Start...()

    public IStoppableWebSocketBehaviour Start()
    {
        Task.Run(StartAndWait);
        return this;
    }
    public IStoppableWebSocketBehaviour StartAndWait()
    {
        if (!_wsbd.HasWebSocket) return this;

        while (_IsWebSocketConnecting())
            Thread.Sleep(10);

        #region PingPong

        if(_wsbd.PingPongProtocolBehaviourDescriptor.HasValidInterval)
            _tPingPong =
                Task.Run
                (
                    async () =>
                    {
                        Byte[]? ba;
                        BlinkOnPongReceived();

                        while (_IsWebSocketOpened())
                        {
                            if (_wsbd.PingPongProtocolBehaviourDescriptor.HasOnSendCustomPacket)
                                await SendPacketAsync(_wsbd.PingPongProtocolBehaviourDescriptor.OnSendCustomPacket());
                            else
                                await SendPacketAsync(__baPing);

                            Thread.Sleep(_wsbd.PingPongProtocolBehaviourDescriptor.Interval.Value);

                            if (Math.Abs(UnixTimeStamp.GetCurrent() - _iPong) > _wsbd.PingPongProtocolBehaviourDescriptor.Interval.Value.TotalMilliseconds)
                                await _CloseAsync(WebSocketCloseStatus.EndpointUnavailable, __sEndpointUnavailable);
                        }
                    }
                );

        #endregion

        Task<WebSocketReceivePacket?> tReceiving;
        WebSocketReceivePacket? wsrp;
        String? scsd = null;
        WebSocketCloseStatus? wscs = null;

        while (_IsWebSocketOpened())
        {
            tReceiving = _ReceivePacketAsync();
            tReceiving.Wait();
            wsrp = tReceiving.Result;

            if (_wsbd.CancellationToken.IsCancellationRequested)
            {
                scsd = nameof(WebSocketCloseStatus.NormalClosure);
                wscs = WebSocketCloseStatus.NormalClosure;
                break;
            }
            else if (wsrp == null)
                continue;
            else if (wsrp.Result.CloseStatus != null)
            {
                scsd = wsrp.Result.CloseStatusDescription;
                wscs = wsrp.Result.CloseStatus;
                break;
            }
            else if (_wsbd.HasOnReceivePacket)
                try { _wsbd.OnReceivePacket(this, wsrp.Bytes); } catch { }
        }

        _CloseAsync(wscs.Value, scsd).Wait();

        return this;
    }

    #endregion

    #region public ... Stop...()

    public void Stop() { Task t = StopAsync(); t.Wait(); }
    public async Task StopAsync()
    {
        await _CloseAsync( WebSocketCloseStatus.NormalClosure, __sNormalClosure );
    }

    #endregion

    #region public async Task SendPacketAsync(...)

    /*
     * String? s = o as String;
        if (s != null) { await SendPacketAsync(s); return; }
        Byte[]? ba = o as Byte[];
        if (ba != null) { await SendPacketAsync(ba); return; }
    */

    //public async Task SendPacketAsync(Object? o) { await SendPacketAsync(ObjectUtils.Parse<String>(o)); }
    //public async Task SendPacketAsync(Object? o, JsonSerializerOptions? jsonso)
    //{
    //    if (jsonso != null) await SendPacketAsync(JSONUtils.Serialize(o, jsonso));
    //    else await SendPacketAsync(o);
    //}
    //public async Task SendPacketAsync(String? s) { await SendPacketAsync(BytesUtils.Parse(s)); }
    //public async Task SendPacketAsync(String? s, Encoding? enc) { await SendPacketAsync(BytesUtils.Parse(s, enc)); }
    public async Task SendPacketAsync(Byte[]? ba)
    {
        if (ba == null || !_IsWebSocketOpened()) return;
        try { await _wsbd.WebSocket.SendAsync(ba, WebSocketMessageType.Text, true, _wsbd.CancellationToken); } catch { }
    }

    #endregion

    #region private async Task<Byte[]?> _ReceivePacketAsync(...)

    private async Task<WebSocketReceivePacket?> _ReceivePacketAsync()
    {
        if (!_wsbd.HasValidReadBufferSize || !_IsWebSocketOpened()) return null;
        Byte[] baRead = new byte[_wsbd.ReadBufferSize.Value];
        WebSocketReceiveResult? wsrr;
        try { wsrr = await _wsbd.WebSocket.ReceiveAsync(baRead, _wsbd.CancellationToken); }
        catch(Exception e)
        {
            Exception prova = e;
            wsrr = null;
        }
        return
            wsrr != null
            ? new WebSocketReceivePacket(ref wsrr, ref baRead)
            : null;
    }

    #endregion

    public void BlinkOnPongReceived()
    {
        _iPong = UnixTimeStamp.GetCurrent();
    }

    #region public async Task _CloseAsync(...)

    private async Task _CloseAsync(WebSocketCloseStatus wscs)
    {
        await _CloseAsync(wscs, null);
    }
    private async Task _CloseAsync(WebSocketCloseStatus wscs, String? sDescription)
    {
        if (!_IsWebSocketOpened()) return;
        try { await _wsbd.WebSocket.CloseAsync(wscs, sDescription, _wsbd.CancellationToken); } catch { }
       // _StopPingPongProtocol();
    }

    #endregion

    #region private Boolean _IsWebSocketOpened()

    private Boolean _IsWebSocketOpened()
    {
        return
            _wsbd.HasWebSocket
            && _wsbd.WebSocket.State == System.Net.WebSockets.WebSocketState.Open;
    }

    #endregion

    #region private Boolean _IsWebSocketConnecting()

    private Boolean _IsWebSocketConnecting()
    {
        return
            _wsbd.HasWebSocket
            && _wsbd.WebSocket.State == System.Net.WebSockets.WebSocketState.Connecting;
    }

    #endregion

    #region private Boolean _IsWebSocketClosed()

    private Boolean _IsWebSocketClosed()
    {
        return
            _wsbd.HasWebSocket
            &&
            (
                _wsbd.WebSocket.State == System.Net.WebSockets.WebSocketState.Closed
                || _wsbd.WebSocket.State == System.Net.WebSockets.WebSocketState.CloseReceived
                || _wsbd.WebSocket.State == System.Net.WebSockets.WebSocketState.CloseSent
            );
    }

    #endregion

    #region private Boolean _IsWebSocketAborted()

    private Boolean _IsWebSocketAborted()
    {
        return
            _wsbd.HasWebSocket
            && _wsbd.WebSocket.State == System.Net.WebSockets.WebSocketState.Aborted;
    }

    #endregion
}