using SOV.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.Social
{
    public static class TableFields
    {
        public static List<TableField> Image()
        {
            return new List<TableField>()
            {
                new TextTableField("Id", "id", "Id") {inInsert = false, LockedUpdate = true},
                new ImageInputTableField("Изображение", "img", "Img")
            };
        }

        public static List<TableField> LegalEntity()
        {
            return new List<TableField>()
            {
                new TextTableField("Id", "id", "Id") {inInsert = false, LockedUpdate = true},
                new TextTableField("Рус. Имя", "name_rus", "NameRus"),
                new TextTableField("Анг. Имя", "name_eng", "NameEng"),
                new TextTableField("Сокр. Рус. Имя", "name_rus_short", "NameRusShort"),
                new TextTableField("Сокр. Анг. Имя", "name_eng_short", "NameEngShort"),
                new TreeTableField("Адрес", "address_id", "AddrId", "FullAddr"),
                new TextTableField("Дополнение к адресу", "address_add", "AddrAdd"),
                new TextTableField("E-mail", "email", "Email"),
                new TextTableField("Телефон", "phones", "Phones"),
                new TextTableField("Сайт", "web_site", "WebSite"),
                new ComboBoxTableField("Головная организация", "parent_id", "ParentId", "Parent"),
                new ComboBoxTableField("Тип", "type", "Type", "TypeName") {items = Social.LegalEntity.Types, LockedUpdate = true}
            };
        }

        public static List<TableField> Org()
        {
            return new List<TableField>()
            {
                new TextTableField("Id", "legal_entity_id", "LegalEntityId") {inInsert = false, HiddenUpdate = true},
                new ComboBoxTableField("Директор", "staff_id_first_face", "StaffIdFirstFace")
            };
        }
        
        public static List<TableField> Person()
        {
            return new List<TableField>()
            {
                new TextTableField("Id", "legal_entity_id", "LegalEntityId") {inInsert = false, HiddenUpdate = true},
                new ComboBoxTableField("Пол", "sex", "Sex")
            };
        }

        public static List<TableField> Staff()
        {
            return new List<TableField>()
            {
                new TextTableField("Id", "id", "Id") {inInsert = false, LockedUpdate = true},
                new ComboBoxTableField("Работадатель", "employer_id", "EmployerId", "Employer"),
                new ComboBoxTableField("Работник", "employee_id", "EmployeeId", "Employee"),
                new ComboBoxTableField("Должность", "staff_position_id", "StaffPositionId", "StaffPosition")
            };
        }

        public static List<TableField> StaffPosition()
        {
            return new List<TableField>()
            {
                new TextTableField("Id", "id", "Id") { inInsert = false, LockedUpdate = true },
                new TextTableField("Рус. Имя", "name_rus", "NameRus"),
                new TextTableField("Анг. Имя", "name_eng", "NameEng"),
                new TextTableField("Сокр. Рус. Имя", "name_rus_short", "NameRusShort"),
                new TextTableField("Сокр. Анг. Имя", "name_eng_short", "NameEngShort"),
            };
        }
    }
}
