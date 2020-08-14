using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Common
{
    public class Block
    {
        public Block()
        {

        }

        public static void Save(Entity.Common.Block block)
        {
            using (DataManager oDm = new DataManager())
            {
                oDm.Add("@pBlockId", SqlDbType.Int, ParameterDirection.InputOutput, block.BlockId);
                oDm.Add("@pBlockName", SqlDbType.VarChar, 50, block.BlockName);
                oDm.Add("@pDistrictId", SqlDbType.Int, block.DistrictId);
                oDm.Add("@pCode", SqlDbType.VarChar, 20, block.Code);
                if (block.Remarks == string.Empty)
                    oDm.Add("@pRemarks", SqlDbType.VarChar, 200, DBNull.Value);
                else
                    oDm.Add("@pRemarks", SqlDbType.VarChar, 200, block.Remarks);


                oDm.CommandType = CommandType.StoredProcedure;

                oDm.ExecuteNonQuery("Block_Save");

                block.BlockId = (int)oDm["@pBlockId"].Value;
            }
        }

        public static DataTable GetAll(int districtid, int stateid)
        {
            using (DataManager oDm = new DataManager())
            {
                if (districtid == 0)
                    oDm.Add("@DistrictId", SqlDbType.Int, ParameterDirection.Input, DBNull.Value);
                else
                    oDm.Add("@DistrictId", SqlDbType.Int, ParameterDirection.Input, districtid);

                if (stateid == 0)
                    oDm.Add("@StateId", SqlDbType.Int, ParameterDirection.Input, DBNull.Value);
                else
                    oDm.Add("@StateId", SqlDbType.Int, ParameterDirection.Input, stateid);

                oDm.CommandType = CommandType.StoredProcedure;
                return oDm.ExecuteDataTable("Block_GetAll");
            }
        }

        public static Entity.Common.Block GetBlockById(int blockId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pBlockId", SqlDbType.Int, ParameterDirection.Input, blockId);

                SqlDataReader dr = oDm.ExecuteReader("Block_GetById");

                Entity.Common.Block block = new Entity.Common.Block();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        block.BlockId = blockId;
                        block.BlockName = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                        block.DistrictId = (dr[2] == DBNull.Value) ? 0 : int.Parse(dr[2].ToString());
                        block.Code = (dr[3] == DBNull.Value) ? "" : dr[3].ToString();
                        block.Remarks = (dr[4] == DBNull.Value) ? "" : dr[4].ToString();

                    }
                }
                return block;
            }
        }

        public static void Delete(int blockId)
        {
            using (DataManager oDm = new DataManager())
            {

                oDm.CommandType = CommandType.StoredProcedure;

                oDm.Add("@pBlockId", SqlDbType.Int, ParameterDirection.Input, blockId);

                oDm.ExecuteNonQuery("Block_Delete");
            }
        }
    }
}