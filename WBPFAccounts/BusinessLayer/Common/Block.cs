using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer.Common
{
	public class Block
	{
		public Block()
		{

		}

        public void Save(Entity.Common.Block block)
		{
            DataAccess.Common.Block.Save(block);
		}

		public DataTable GetAll(int districtid, int stateid)
		{
            return DataAccess.Common.Block.GetAll(districtid, stateid);
		}

        public Entity.Common.Block GetBlockById(int blockId)
		{
            return DataAccess.Common.Block.GetBlockById(blockId);
		}

		public void Delete(int blockId)
		{
            DataAccess.Common.Block.Delete(blockId);
		}
	}
}