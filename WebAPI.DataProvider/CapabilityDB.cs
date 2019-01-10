using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataProvider.EF;

namespace WebAPI.DataProvider
{
    public class CapabilityDB : IAction<SYS_Capability, string>
    {
        public SYS_Capability Create(SYS_Capability model)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    if (model != null)
                    {
                        var addModel = context.SYS_Capability.Add(model);
                        var result = context.SaveChanges();
                        if (result > 0)
                        {
                            return addModel;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    var model = context.SYS_Capability.FirstOrDefault(f => f.ID.Equals(id));
                    if (model != null)
                    {
                        context.SYS_Capability.Remove(model);
                        return context.SaveChanges() > 0;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SYS_Capability Get(string id)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.SYS_Capability.FirstOrDefault(f => f.ID.Equals(id));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<SYS_Capability> GetAll()
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.SYS_Capability.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(string id, SYS_Capability model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                using (var context = new SystemEntities())
                {
                    var baseModel = context.SYS_Capability.FirstOrDefault(f => f.ID.Equals(id));
                    if (baseModel == null)
                    {
                        throw new NullReferenceException("Không tìm thấy đối tượng trong cơ sở dữ liệu");
                    }
                    else
                    {
                        model.ID = id;
                        context.Entry(baseModel).CurrentValues.SetValues(model);
                        //context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        //context.SYS_Layer.Attach(model);
                        return context.SaveChanges() > 0;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
