using EmployeeCleanArch.Application.DTOs;
using EmployeeCleanArch.Domain.Entities;
using Mapster;


namespace MyCleanApp.Application.Mappings
{
    public class MappingConfig
    {
        public static void ConfigureMappings()
        {
            TypeAdapterConfig<CreateDepartmentDTO, Department>.NewConfig();
            TypeAdapterConfig<UpdateDepartmentDTO, Department>.NewConfig();
            TypeAdapterConfig<CreateEmployeeDTO, Employee>.NewConfig();
            TypeAdapterConfig<UpdateEmployeeDTO, Employee>.NewConfig();
            //TypeAdapterConfig<GetEmployeeDTO, Employee>.NewConfig();
            TypeAdapterConfig<Employee, GetEmployeeDTO>.NewConfig()
                .Map(dest => dest.Gender, src => src.Gender.ToString().ToLower());
        }
    }
}
