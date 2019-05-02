using System;
using System.Collections.Generic;
using System.Linq;
using Import.AmurServiceReference;
using FERHRI.Common;
using Npgsql;

namespace Import
{
    class KH02Dictionaryes201702
    {
        /// <summary>
        /// Импорт справочников КН-02 из БД Истомина Д. П. 2017.02.03
        /// </summary>
        /// <param name="filePaths"></param>
        internal static void Import(ServiceClient client, long hSvc, string connectionStringSrcDb)
        {
            // Соответствие variableId vs код словаря БД Истомина
            List<int[]> var_x_var = new List<int[]>
                {
                    new int[]{1108,1},
                    new int[]{1107,2},
                    new int[]{1061,3},
                    new int[]{1063,4},
                    new int[]{1064,5},
                    new int[]{1102,6},
                    new int[]{1103,6},
                    new int[]{1104,7},
                    new int[]{1105,8},
                    new int[]{1065,9},
                    new int[]{1070,10},
                    new int[]{1071,11},
                    new int[]{1075,11},
                    new int[]{1076,12},
                    new int[]{1079,11},
                    new int[]{1080,12},
                    new int[]{1081,13},
                    new int[]{1082,14},
                    new int[]{1083,15},
                    new int[]{1084,16},
                    new int[]{1085,17},
                    new int[]{1087,18},
                    new int[]{1088,16},
                    new int[]{1089,17},
                    new int[]{1090,18},
                    new int[]{1091,19},
                    new int[]{1092,20},
                    new int[]{1093,12},
                    new int[]{1094,21},
                    new int[]{1095,22},
                    new int[]{1096,23},
                    new int[]{1097,24}
                };

            // Выбрать данные Истомина
            Dictionary<Dic, List<Dic>> dic = new Dictionary<Dic, List<Dic>>();

            using (NpgsqlConnection cnn = (new ADbNpgsql(connectionStringSrcDb)).Connection)
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from sea.typecodetables", cnn))
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            dic.Add(new Dic() { Id = (int)rdr["id"], Name = rdr["description"].ToString() }, new List<Dic>());
                        }
                    }

                    cmd.CommandText = "select * from sea.codetables where typecodetables_id = :typecodetables_id";
                    cmd.Parameters.AddWithValue("typecodetables_id", 0);
                    foreach (KeyValuePair<Dic, List<Dic>> kvp in dic)
                    {
                        Console.WriteLine("код исходного словаря {0} name {1}.", kvp.Key.Id, kvp.Key.Name);
                        if (var_x_var.FirstOrDefault(x => x[1] == kvp.Key.Id) == null)
                            throw new Exception("Отсутствует код исходного словаря id = " + kvp.Key.Id + " name = " + kvp.Key.Name);

                        cmd.Parameters[0].Value = kvp.Key.Id;
                        using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                string code = rdr["code"].ToString();
                                if (code != "/" && code != "//")
                                    kvp.Value.Add(new Dic() { Id = int.Parse(code), Name = rdr["value"].ToString(), NameShort = rdr["shortvalue"].ToString() });
                            }
                        }
                        if (kvp.Value.Count == 0)
                            throw new Exception("Отсутствует записи значений для кода исходного словаря id = " + kvp.Key + " name = " + kvp.Value);
                    }
                }
            }

            // Записать в Амур
            foreach (KeyValuePair<Dic, List<Dic>> kvp in dic)
            {
                foreach (int[] vcid in var_x_var.FindAll(x => x[1] == kvp.Key.Id))
                {
                    int variableId = vcid[0];
                    if (client.GetVariableCodesAll(hSvc, variableId).Count() == 0)
                        foreach (Dic item in kvp.Value)
                        {
                            VariableCode vc = new VariableCode()
                            {
                                VariableId = variableId,
                                Code = item.Id,
                                Name = item.Name,
                                NameShort = string.IsNullOrEmpty(item.NameShort)
                                    ? item.Name.Substring(0, item.Name.Length > 50 ? 50 : item.Name.Length) : item.NameShort,
                                Description = item.Name
                            };

                            client.SaveVariableCode(hSvc, vc);
                        }
                }
            }
        }
        class Dic
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string NameShort { get; set; }
        }
    }
}
