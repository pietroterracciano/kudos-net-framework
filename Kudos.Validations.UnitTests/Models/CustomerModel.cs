using System;
using Kudos.Constants;
using Kudos.Validations.EpikyrosiModule.Attributes;

namespace Kudos.Validations.UnitTests.Models
{
    public class CustomerModel
    {
        [EpikyrosiNumericRuleAttribute<Int32>(PoolName = "REGOLA_PER_ADD", MinLength = 32)]
        [EpikyrosiNumericRuleAttribute<Int32>(PoolName = "REGOLA_PER_EDIT", MinLength = 32)]
        public UInt32? ID { get; set; }


        [EpikyrosiStringRule(PoolName = "REGOLA_PER_ADD", CanBeNull = false, MinLength = 32)]
        [EpikyrosiStringRule(PoolName = "REGOLA_PER_EDIT", CanBeNull = false, MaxLength = 32)]
        public String Ciao { get; set; }

        [EpikyrosiStringRule(PoolName = "REGOLA_PER_ADD", MinLength = 32)]
        [EpikyrosiStringRule(PoolName = "REGOLA_PER_EDIT", CanBeNull = false, MaxLength = 32)]
        public String FirstName;

        public String LastName;

        public Boolean IsDeleted;
    }
}