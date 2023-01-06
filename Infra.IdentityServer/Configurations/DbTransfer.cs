using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace Infra.IdentityServer.Configurations
{
    public class DbTransfer
    {
        public static void Patch(ConfigurationDbContext _context) 
        {
            //clients
            foreach (var cl in Configs.Clients.ExceptBy(_context.Clients.Select(p => p.ClientId).ToList(), p => p.ClientId))
            {
                _context.Add(cl.ToEntity());
            }
            _context.SaveChanges();
            //resourceIdentities
            foreach (var ir in Configs.IdentityResources.ExceptBy(_context.IdentityResources.Select(p => p.Name).ToList(), p => p.Name))
            {
                _context.Add(ir.ToEntity());
            }
            _context.SaveChanges();
            //scopedapi
            foreach (var ir in Configs.ApiScopes.ExceptBy(_context.ApiScopes.Select(p => p.Name).ToList(), p => p.Name))
            {
                _context.Add(ir.ToEntity());
            }
            _context.SaveChanges();
        }
        public static void Put(ConfigurationDbContext _context)
        {
            //clients
            _context.RemoveRange(_context.Clients.ToList());
            _context.SaveChanges();
            foreach (var cl in Configs.Clients.ExceptBy(_context.Clients.Select(p => p.ClientId).ToList(), p => p.ClientId))
            {
                _context.Add(cl.ToEntity());
            }
            _context.SaveChanges();
            //resourceIdentities
            _context.RemoveRange(_context.IdentityResources.ToList());
            _context.SaveChanges();
            foreach (var ir in Configs.IdentityResources.ExceptBy(_context.IdentityResources.Select(p => p.Name).ToList(), p => p.Name))
            {
                _context.Add(ir.ToEntity());
            }
            _context.SaveChanges();
            //scopedapi
            _context.RemoveRange(_context.ApiScopes.ToList());
            _context.SaveChanges();
            foreach (var ir in Configs.ApiScopes.ExceptBy(_context.ApiScopes.Select(p => p.Name).ToList(), p => p.Name))
            {
                _context.Add(ir.ToEntity());
            }
            _context.SaveChanges();
        }
    }
}
