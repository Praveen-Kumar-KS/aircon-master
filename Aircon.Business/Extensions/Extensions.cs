using System;
using System.Linq;
using System.Reflection;
using Aircon.Business.Models.Shared;
using Aircon.Data.Entities;

namespace Aircon.Business.Extensions
{
    public static class Extensions
    {
        public static T GetLookupEntity<T>(this LookupModel obj) where T : LookupEntity
        {
            try
            {
                return (T)typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
            catch
            {
                return default(T);
            }
        }

        public static T GetAttachmentEntity<T>(this AttachmentListModel obj) where T : AttachmentEntity
        {
            try
            {
                return (T)typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
            catch
            {
                return default(T);
            }
        }

        public static T GetNoteEntity<T>(this NoteModel obj) where T : NoteEntity
        {
            try
            {
                return (T)typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
            catch
            {
                return default(T);
            }
        }
        public static string GetDescription(this Enum GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return GenericEnum.ToString();
        }
    }
}
