/// Serviceインターフェイスとその実装

using WebApp_Src.Applications.Domains;
namespace WebApp_Src.Applications.Services;

public interface IDepartmentUpdateService
{
    Department UpdateById(int id);
    void Update(Department department);
    bool Exists(string name);
}