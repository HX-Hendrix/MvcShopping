using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcShopping.Models
{
    [DisplayName("会员信息")]
    [DisplayColumn("Name")]
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("会员账户")]
        [Required(ErrorMessage = "请输入Email地址")]
        [Description("我们直接以Email当成会员的登陆账号")]
        [MaxLength(250, ErrorMessage = "Email地址长度无法超过250个字符")]
        [DataType(DataType.EmailAddress)]
        [Remote("CheckDup", "Member", HttpMethod="POST", ErrorMessage="您输入的Email已经有人注册过了！")]
        public string Email { get; set; }
        [DisplayName("会员密码")]
        [Required(ErrorMessage = "请输入密码")]
        [Description("密码将以SHA1进行哈希运算，通过SHA1哈希运算后的结果转为HEX表示法的字符串长度皆为40个字符")]
        [MaxLength(40, ErrorMessage = "密码不得超过40个字符")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("中文姓名")]
        [Required(ErrorMessage = "请输入中文姓名")]
        [Description("暂不考录有外国人用英文注册会员的情况")]
        [MaxLength(5, ErrorMessage = "中文姓名不得超过5个字")]
        public string Name { get; set; }
        [DisplayName("网络昵称")]
        [Required(ErrorMessage = "请输入网络昵称")]
        [MaxLength(10, ErrorMessage = "网络昵称请勿输入超过10个字")]
        public string Nickname { get; set; }
        [DisplayName("会员注册时间")]
        public DateTime RegisterOn { get; set; }
        [DisplayName("会员启用认证码")]
        [Description("当AuthCode等于null则代表此会员已经通过Email有效性验证")]
        [MaxLength(36)]
        public string AuthCode { get; set; }

        public virtual ICollection<OrderHeader> Orders { get; set; }
    }
}