using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataProvider.EF;
using WebAPI.DataProvider.Models;

namespace WebAPI.DataProvider
{
    public class LayerRoleDB : IAction<SYS_Layer_Role, int>
    {
        public SYS_Layer_Role Create(SYS_Layer_Role model)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    if (model != null)
                    {
                        var addModel = context.SYS_Layer_Role.Add(model);
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

        public bool Delete(int id)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    var model = context.SYS_Layer_Role.FirstOrDefault(f => f.ID.Equals(id));
                    if (model != null)
                    {
                        context.SYS_Layer_Role.Remove(model);
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

        public SYS_Layer_Role Get(int id)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.SYS_Layer_Role.FirstOrDefault(f => f.ID.Equals(id));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SYS_Layer_Role Get(string role, string layer)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.SYS_Layer_Role.FirstOrDefault(f => 
                    (String.IsNullOrEmpty(role) || (!String.IsNullOrEmpty(role) && f.Role.Equals(role)))
                    && (String.IsNullOrEmpty(layer) || (!String.IsNullOrEmpty(layer) && f.Layer.Equals(layer)))
                    );
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<SYS_Layer_Role> GetAll()
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.SYS_Layer_Role.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(int id, SYS_Layer_Role model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                using (var context = new SystemEntities())
                {
                    var baseModel = context.SYS_Layer_Role.FirstOrDefault(f => f.ID.Equals(id));
                    if (baseModel == null)
                    {
                        throw new NullReferenceException("Không tìm thấy đối tượng trong cơ sở dữ liệu");
                    }
                    else
                    {
                        model.ID = id;
                        context.Entry(baseModel).CurrentValues.SetValues(model);
                        //context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        //context.SYS_Layer_Role.Attach(model);
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
