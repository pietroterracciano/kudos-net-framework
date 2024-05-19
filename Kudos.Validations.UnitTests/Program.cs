
using System.Diagnostics;
using Kudos.Validations.EpikyrosiModule;
using Kudos.Validations.EpikyrosiModule.Interfaces.Builders;
using Kudos.Validations.EpikyrosiModule.Interfaces.Builds;
using Kudos.Validations.EpikyrosiModule.Interfaces.Entities;
using Kudos.Validations.EpikyrosiModule.Results;
using Kudos.Validations.EpikyrosiModule.Rules;
using Kudos.Validations.UnitTests.Models;


CustomerModel prova = new CustomerModel();
prova.ID = 0;


EpikyrosiResult
    RISULTATO = 
        Epikyrosi.Validate(prova, "REGOLA_PER_ADD");

Epikyrosi.Validate(prova, "REGOLA_PER_ADD");
Epikyrosi.Validate(prova, "REGOLA_PER_ADD");
RISULTATO = Epikyrosi.Validate(null, "REGOLA_PER_ADD");

Epikyrosi.Validate(prova, "REGOLA_PER_ADD");








IEpikyrosiEntity
    ee =
        Epikyrosi.RequestEntity<CustomerModel>("ID");

IEpikyrosiBuilder
    eb =
        Epikyrosi
            .RequestBuilder()
                .AddNumericRule(ee, new EpikyrosiNumericRule<Int32>() { CanBeNull = false, MinValue = 5 })
                .AddStringRule(ee, new EpikyrosiStringRule() { CanBeNull = false, CanBeEmpty = false });

IEpikyrosiBuilt
    ebt = eb.Build();

prova.ID = 0;

Stopwatch sw = new Stopwatch(); sw.Start();
for(int i=0; i<1000000; i++)
{ 
    EpikyrosiResult er =  ebt.Validate(prova);
}

sw.Stop();

long g = sw.ElapsedMilliseconds;

Boolean b = true;