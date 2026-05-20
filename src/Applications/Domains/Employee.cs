/// ドメインオブジェクト
/// データの意味と振る舞いを定義する
/// 
using WebApp_Src.Exceptions;
namespace WebApp_Src.Applications.Domains;
/// <summary>
/// 従業員を表すドメインオブジェクト
/// </summary>
public class Employee
{
    public int? Id { get; private set; } // 社員Id
    public string Name { get; private set; } = string.Empty; // 氏名
    public string Mailadress { get; private set; } = string.Empty; // メールアドレス
    public string Phonenumber { get; private set; } = string.Empty; // 電話番号
    public Department? Department { get; private set; } // 所属部署（null可）

    private const int MaxLength = 20;
    private const int MaxLengthMail = 50;

    /// <summary>
    /// コンストラクタ
    /// </summary>

    public Employee(int? id, string name, string mailadress, string phonenumber, Department? department)
    {
        ValidateName(name);
        Id = id;
        Name = name;
        Mailadress = mailadress;
        Phonenumber = phonenumber;
        Department = department;
    }

    /// <summary>
    /// ID未定の社員を作成する場合のコンストラクタ
    /// </summary>
    public Employee(string name, string mailadress, string phonenumber, Department? department)
        : this(null, name, mailadress, phonenumber, department) { }

    /// <summary>
    /// 氏名の検証
    /// </summary>
    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("氏名は必須です");
        if (name.Length > MaxLength)
            throw new DomainException($"氏名は{MaxLength}文字以内で入力してください");
    }
    /// <summary>
    /// メールアドレスの検証
    /// </summary>
    private void ValidateMailAdress(string mailadress)
    {
        if (string.IsNullOrWhiteSpace(mailadress))
            throw new DomainException("メールアドレスは必須です");
        if (mailadress.Length > MaxLengthMail)
            throw new DomainException($"メールアドレスは{MaxLengthMail}文字以内で入力してください");
    }
    /// <summary>
    /// 電話番号の検証
    /// </summary>
    private void ValidatePhoneNumber(string phonenumber)
    {
        if (string.IsNullOrWhiteSpace(phonenumber))
            throw new DomainException("電話番号は必須です");
        if (phonenumber.Length > MaxLength)
            throw new DomainException($"電話番号は{MaxLength}文字以内で入力してください");
    }
    /// <summary>
    /// 氏名を変更する
    /// </summary>
    public void ChangeName(string name)
    {
        ValidateName(name);
        Name = name;
    }

    /// <summary>
    /// メールアドレスを変更する
    /// </summary>
    public void ChangeMailAdress(string mailadress)
    {
        ValidateMailAdress(mailadress);
        Mailadress = mailadress;
    }
    /// <summary>
    /// 電話番号を変更する
    /// </summary>
    public void ChangePhoneNumber(string phonenumber)
    {
        ValidatePhoneNumber(phonenumber);
        Phonenumber = phonenumber;
    }
    /// <summary>
    /// 所属部署を変更する
    /// </summary>
    public void ChangeDepartment(Department? department)
    {
        Department = department;
    }

    /// <summary>
    /// 等価性（IDによる比較）
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not Employee other) return false;
        return Id == other.Id;
    }

    public override int GetHashCode() => Id?.GetHashCode() ?? 0;

    public override string ToString()
        => $"{Id?.ToString() ?? "未登録"}: {Name}/ {Id?.ToString() ?? "未登録"}: {Mailadress}/ {Id?.ToString() ?? "未登録"}: {Phonenumber} / {Department?.Name ?? "未配属"}";
}