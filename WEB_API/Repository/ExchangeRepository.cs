using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Oracle.ManagedDataAccess.Client;
using WEB_API.Context;
using WEB_API.Model;

namespace WEB_API.Repository
{
    public class ExchangeRepository : IExchangeRepository
    {
        private IDBContext _dbContext;

        public ExchangeRepository(IDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Exchange> GetAllBranches()
        {
            List<Exchange> exchanges = new List<Exchange>();

            using (OracleConnection con = _dbContext.GetConn())
            {
                using (OracleCommand cmd = _dbContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = @"SELECT
                                                C.CC_ID,
                                                TO_CHAR(C.CC_DAY, 'dd.mm.yyyy') CC_DAY,
                                                I.IN_NAME,
                                                C.CC_OPENKURS,
                                                C.CC_HIGHKURS,
                                                C.CC_LOWKURS,
                                                C.CC_CLOSEKURS,
                                                C.CC_VALUE
                                            FROM TB_CHART_CANDLE C
                                            LEFT JOIN TB_INSTRUMENT I ON I.IN_ID=C.CC_INSTRUMENT
                                            WHERE CC_DAY>TO_DATE('01.01.2023', 'dd.mm.yyyy') AND C.CC_FRAME=3
                                            ORDER BY CC_ID";

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            exchanges.Add(new Exchange()
                            {
                                Id = Convert.ToInt32(reader["CC_ID"]),
                                Day = reader["CC_DAY"].ToString(),
                                Instrument = reader["IN_NAME"].ToString(),
                                OpenKurs = Convert.ToInt32(reader["CC_OPENKURS"]),
                                HighKurs = Convert.ToInt32(reader["CC_HIGHKURS"]),
                                LowKurs = Convert.ToInt32(reader["CC_LOWKURS"]),
                                CloseKurs = Convert.ToInt32(reader["CC_CLOSEKURS"]),
                                Value = Convert.ToInt32(reader["CC_VALUE"])
                            });
                        }
                        reader.Dispose();
                        return exchanges;
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        public List<Exchange> GetFindBranches(string date)
        {            
            List<Exchange> exchanges = new List<Exchange>();

            using (OracleConnection con = _dbContext.GetConn())
            {
                using (OracleCommand cmd = _dbContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = $@"SELECT
                                                C.CC_ID,
                                                TO_CHAR(C.CC_DAY, 'dd.mm.yyyy') CC_DAY,
                                                I.IN_NAME,
                                                C.CC_OPENKURS,
                                                C.CC_HIGHKURS,
                                                C.CC_LOWKURS,
                                                C.CC_CLOSEKURS,
                                                C.CC_VALUE
                                            FROM TB_CHART_CANDLE C
                                            LEFT JOIN TB_INSTRUMENT I ON I.IN_ID=C.CC_INSTRUMENT
                                            WHERE CC_DAY>TO_DATE('01.01.2023', 'dd.mm.yyyy') AND C.CC_FRAME=3 AND C.CC_DAY=TO_DATE('{date} 15:00:00', 'dd.mm.yyyy hh24:mi:ss')
                                            ORDER BY CC_ID";

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            exchanges.Add(new Exchange()
                            {
                                Id = Convert.ToInt32(reader["CC_ID"]),
                                Day = reader["CC_DAY"].ToString(),
                                Instrument = reader["IN_NAME"].ToString(),
                                OpenKurs = Convert.ToInt32(reader["CC_OPENKURS"]),
                                HighKurs = Convert.ToInt32(reader["CC_HIGHKURS"]),
                                LowKurs = Convert.ToInt32(reader["CC_LOWKURS"]),
                                CloseKurs = Convert.ToInt32(reader["CC_CLOSEKURS"]),
                                Value = Convert.ToInt32(reader["CC_VALUE"])
                            });
                        }
                        reader.Dispose();
                        return exchanges;
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
        public List<Exchange> PostExchangeBranches(List<Exchange> exchange)
        {
            
            int instrumentCode;
            switch (exchange.) 
            {
                case "USD":
                    instrumentCode = 1;
                    break;
                case "EUR":
                    instrumentCode = 2;
                    break;
                case "RUB":
                    instrumentCode = 3;
                    break;
                case "CNY":
                    instrumentCode = 4;
                    break;
                default:
                    instrumentCode = 0; 
                    break;
            }

            using (OracleConnection con = _dbContext.GetConn())
            {
                using (OracleCommand cmd = _dbContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = $@"INSERT INTO TB_CHART_CANDLE
                                                (CC_DAY, CC_INSTRUMENT, CC_FRAME, CC_OPENKURS, CC_HIGHKURS, CC_LOWKURS, CC_CLOSEKURS, CC_VALUE) 
                                             VALUES
                                                (TO_DATE('{exchange.Day} 15:00:00', 'dd.mm.yyyy hh24:mi:ss', {instrumentCode}, {3}, {exchange.OpenKurs}, {exchange.HighKurs}, {exchange.LowKurs}, {exchange.CloseKurs}, {exchange.Value})";

                        //Execute the command and use DataReader to display the data
                        cmd.ExecuteNonQuery();

                        return exchanges;
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}
