using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataProvider.EF;
using WebAPI.DataProvider.Models;

/**
* @author phuonghieuho
*
* @date - 11/20/2018 11:50:37 AM 
*/

namespace WebAPI.DataProvider.GIS
{
    public class HanhChinhDB
    {
        public HANHCHINH Create(HANHCHINH model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public HANHCHINH Get(string id)
        {
            try
            {
                using (var context = new GISEntities())
                {
                    return context.HANHCHINHs.FirstOrDefault(f => f.IDHanhChinh.Equals(id));
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public IEnumerable<HANHCHINH> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(string id, HANHCHINH model)
        {
            throw new NotImplementedException();
        }
        public HANHCHINH GetByPoint(Point point)
        {
            using (var context = new GISEntities())
            {
                var query = Helper.Query(@"
                    DECLARE @g geometry;  
                    SET @g = geometry::STGeomFromText('POINT ({0} {1})', 0); 

                    select * FROM sde.HANHCHINH a where a.SHAPE.STContains(@g) = 1 
                    ");
                var model = context.Database.SqlQuery<HANHCHINH>(String.Format(query, point.x, point.y)).FirstOrDefault();
                if (model != null)
                {
                    model.SHAPE = null;
                }
                return model;
            }
        }
    }
}
