using AutoMapper;
using webApp.Dtos;

namespace webApp.MapperConfig
{
    public class Employee:Profile
    {
        public Employee()
        {
            CreateMap<UpdateEmployeeDTO, Employee>();
      
        }
    }
}
