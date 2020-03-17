using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PersonelTuruEnumHelper
    {
        public static string GetText(byte id)
        {
            switch (id)
            {
                case 1:
                    //PersonelTuruEnum.Calisan
                    return "Çalışan";
                    break;
                case 2:
                    //PersonelTuruEnum.BirimYoneticisi
                    return "Birim Yöneticisi";
                    break;
                case 3:
                    //PersonelTuruEnum.Yonetici
                    return "Yönetici";
                    break;
                case 4:
                    //PersonelTuruEnum.Muhasebe
                    return "Muhasebe";
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }

        public static string GetText(PersonelTuruEnum e)
        {
            switch (e)
            {
                case PersonelTuruEnum.Calisan:
                    return "Calışan";
                    break;
                case PersonelTuruEnum.BirimYoneticisi:
                    return "Birim Yöneticisi";
                    break;
                case PersonelTuruEnum.Yonetici:
                    return "Yönetici";
                    break;
                case PersonelTuruEnum.Muhasebe:
                    return "Muhasebe";
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }
    }
}
