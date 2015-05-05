using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Odbc;

namespace CentralControl
{

    class Database
    {
        protected static SqlDataReader sdr;
        SqlConnection conn;
        string error;
        List<List<int>> errorid;

        public Database()
        {
            conn = new SqlConnection();
            //conn.ConnectionString = "Data Source=LAB229\\SQLEXPRESS; Initial Catalog=mygtl;Integrated Security=True";
            //conn.ConnectionString = ConfigurationSettings.AppSettings["connectionstring"];
            conn.ConnectionString = "Data Source=jiaoda_hxd;Initial Catalog=test2;Integrated Security=True";
        }

        public int insert(string query)
        {
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                error = "connection fail:" + ex.Message;
                return -2;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = query;
            int state = -3;
            try
            {
                state = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                error = "insertion fail:" + ex.Message;
            }
            conn.Close();
            return state;
        }

        public List<List<object>> search(string query)
        {
            List<List<object>> array = new List<List<object>>();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                error = "connection fail:" + ex.Message;
                return null;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = query;
            try
            {
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    List<object> tmparr = new List<object>();
                    Object[] values = new Object[sdr.FieldCount];
                    int fieldCount = sdr.GetValues(values);
                    for (int i = 0; i < fieldCount; i++)
                        tmparr.Add(values[i]);
                    array.Add(tmparr);
                }
            }
            catch (Exception ex)
            {
                error = "search fail:" + ex.Message;
            }
            conn.Close();
            return array;
        }

        public int update(string query)
        {
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                error = "connection fail:" + ex.Message;
                return -2;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = query;
            int state = -3;
            try
            {
                state = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                error = "insertion fail:" + ex.Message;
            }
            conn.Close();
            return state;
        }

        public int insertop(int current1, int current2, int current3, int current4, string barcode, string device_id, int device_state)
        {
            return insert("insert into OP values(" + current1.ToString() + "," + current2.ToString() + "," + current3.ToString() + "," + current4.ToString() + ",'" + barcode + "'," + device_state + ",'" + DateTime.Now.ToString() + "','" + device_id + "')");
        }

        public int insertmb(int current1, int current2, int current3, int current4, string barcode, string device_id, int device_state)
        {
            return insert("insert into MB values(" + current1.ToString() + "," + current2.ToString() + "," + current3.ToString() + "," + current4.ToString() + ",'" + barcode + "'," + device_state + ",'" + DateTime.Now.ToString() + "','" + device_id + "')");
        }

        public int insertlog(string dev_operator, string device_id, string direction)
        {
            return insert("insert into OPERATELOG values('" + dev_operator + "','" + DateTime.Now.ToString() + "','" + device_id + "','" + direction + "')");
        }
        public int inserthaclumin(string device_id, int addr, int lumin, int x, int y, int pwm, int device_state)
        {
            return insert("insert into HAC_LUMIN values(" + device_state.ToString() + ",'" + DateTime.Now.ToString() + "','" + device_id.ToString() + "'," + addr.ToString() + "," + lumin.ToString() + "," + x.ToString() + "," + y.ToString() + "," + pwm.ToString() + ")");
        }

        public bool inserthacod(string device_id, /*List<List<int>>*/int[][] value, int device_state)
        {
            int state;
            errorid = new List<List<int>>();
            bool fine = true;
            for (int i = 0; i < 8; i++)
            {
                List<int> line = new List<int>();
                for (int j = 0; j < 12; j++)
                {
                    state = insert("insert into HAC_OD values(" + device_state.ToString() + "," + j.ToString() + "," + i.ToString() + "," + value[i][j].ToString() + ",'" + DateTime.Now.ToString() + "','" + device_id + "')");
                    line.Add(state);
                    if (state != -1)
                    {
                        fine = false;
                    }
                }
                errorid.Add(line);
            }
            return fine;
        }

        public int inserthacengine(string device_id, int elecspeed, int power, int text_speed, int device_state)
        {
            return insert("insert into HAC_ENGINE values(" + device_state.ToString() + "," + text_speed.ToString() + "," + elecspeed.ToString() + "," + power.ToString() + ",'" + DateTime.Now.ToString() + "','" + device_id.ToString() + "')");
        }

        public int inserthacbarcode(string incode, string outcode, string device_id)
        {
            return insert("insert into HAC_BARCODE values('" + DateTime.Now.ToString() + "','" + device_id.ToString() + "','" + incode + "','" + outcode + "')");
        }

        public int inserthactmpmod(string device_id, int device_state, int temperature1, int temperature2, int temperature3, int humidity1, int humidity2)
        {
            return insert("insert into HAC_TMPMOD values(" + temperature1.ToString() + "," + temperature2.ToString() + "," + temperature3.ToString() + "," + humidity1.ToString() + "," + humidity2.ToString() + "," + device_state.ToString() + ",'" + DateTime.Now.ToString() + "','" + device_id.ToString() + "')");
        }


        public int inserthacstate(string device_id, int device_state)
        {
            return insert("insert into HAC_STATE values('" + DateTime.Now.ToString() + "','" + device_id.ToString() + "'," + device_state.ToString() + ")");
        }

        public int insertmmrinfo(string device_id, int module, int temp, int ph, int od)
        {
            return insert("insert into MMR_INFO values('" + DateTime.Now.ToString() + "','" + device_id.ToString() + "'," + module.ToString() + "," + temp.ToString() + "," + ph.ToString() + "," + od.ToString() + ")");
        }

        public int insertlpsplace(string device_id, string source, string target, int quantity, int inspeed, int exspeed, int include, int exclude)
        {
            return insert("insert into LPS_LIQUID values('" + DateTime.Now.ToString() + "','" + device_id.ToString() + "','" + source + "','"
                + target + "'," + quantity.ToString() + "," + inspeed.ToString() + "," + exspeed.ToString() + "," + include.ToString() + "," + exclude.ToString() + ")");
        }

        public int insertlpssetting(string device_id, string setting)
        {
            return insert("insert into LPS_SETTING values('" + DateTime.Now.ToString() + "','" + device_id.ToString() + "'," + setting + ")");
        }

        public List<List<object>> getonlinedevice()
        {
            return search("select * from ONLINEDEVICE");
        }

        public int insertonlinestate(string device_id, string whole)
        {
            List<List<object>> a = new List<List<object>>();
            a = search("select * from ONLINEDEVICE where Device_id='" + device_id + "'");
            if (a.Count > 0)
            {
                return (update("update ONLINEDEVICE set ONLINESTATE=1, CurrentTime='" + DateTime.Now.ToString() + "' where Device_Id='" + device_id + "'")); // not tested
            }
            else
            {
                return (insert("insert into ONLINEDEVICE values('" + DateTime.Now.ToString() + "',1,'" + device_id + "','" + whole + "')"));
            }
        }

        public int insertofflinestate(string device_id)
        {
            return (update("update ONLINEDEVICE set ONLINESTATE=0, CurrentTime='" + DateTime.Now.ToString() + "' where Device_Id='" + device_id + "'"));
        }

        public DataTable getdataset(string device_id)
        {
            DataTable table = new DataTable();
            try
            {
                string commandtext = "select top 50 * from operatelog ";
                if (device_id != "") commandtext = commandtext + " where Device_id='" + device_id + "' order by currenttime desc";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandtext, conn.ConnectionString);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                return table;
            }
            catch (SqlException e)
            {
                error = "getdataseterror";
            }
            table = null;
            return table;
        }

        public string showerror()
        {
            return error;
        }

        public List<List<int>> showmatrixerror()
        {
            return errorid;
        }

    }
}
