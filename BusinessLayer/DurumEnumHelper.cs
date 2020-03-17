using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DurumEnumHelper
    {
        public static string GetText(byte id)
        {
            switch (id)
            {
                case 1:
                    //DurumEnum.OnayBekliyor
                    return "Onay Bekliyor.";
                    break;
                case 2:
                    //DurumEnum.Reddedildi
                    return "Reddedildi. ";
                    break;
                case 3:
                    //DurumEnum.Duzeltilecek:
                    return "Düzeltilecek.";
                    break;
                case 4:
                    //DurumEnum.Odendi:
                    return "Ödendi.";
                    break;
                case 5:
                    //DurumEnum.Odendi:
                    return "Onaylandı.";
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }

        public static string GetText(DurumEnum e)
        {
            switch (e)
            {
                case DurumEnum.OnayBekliyor:
                    return "Onay Bekliyor.";
                    break;
                case DurumEnum.Reddedildi:
                    return "Reddedildi. ";
                    break;
                case DurumEnum.Duzeltilecek:
                    return "Düzeltilecek.";
                    break;
                case DurumEnum.Odendi:
                    return "Ödendi.";
                    break;
                case DurumEnum.Onaylandi:
                    return "Onaylandı.";
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }
    }
}
