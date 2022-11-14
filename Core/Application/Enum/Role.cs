using System.Runtime.Serialization;

namespace Application.Enum
{
    public enum Role
    {
        [EnumMember(Value = "System Admin")]
        SysAdmin,

        [EnumMember(Value = "Administrator")]
        Admin,

        [EnumMember(Value = "User")]
        User
    }
}