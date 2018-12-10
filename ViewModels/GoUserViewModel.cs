
using GoGo.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class GoUserViewModel //: IMapFrom<GoUser>//, IHaveCustomMappings
    {
        public string Id { get; set; }

        public byte[] Image { get; set; }

        public string FirstName { get; set; }


        //public void CreateMappings(IMapperConfigurationExpression configuration)
        //{
        //    
        //}
    }
}
