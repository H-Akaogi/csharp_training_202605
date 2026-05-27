using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Applications.Services;

public interface IDeleteDepartmentService
{
    void Delete(int id);
    Department GetById(int id);
    bool ExistEmployee(int id);
}