using System.DirectoryServices;
using GerenciadoraAD.Model;
using AutoMapper;

namespace GerenciadoraAD.Services
{
    public class ClassMaps
    {
        /// <summary>
        ///  Mapeia uma entrada no AD para um objeto Usuario
        /// </summary>
        /// <param name="entry">Entrada já populada</param>
        /// <returns>retorna um objeto usário com suas propriedades preenchidas</returns>
        public static Usuario EntryToUser(DirectoryEntry entry)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DirectoryEntry, Usuario>()
            .ForMember(dest => dest.sAMAccountName, src => src.MapFrom(a => a.Properties["sAMAccountName"].Value))
            .ForMember(dest => dest.userPrincipalName, src => src.MapFrom(a => a.Properties["userPrincipalName"].Value))
            .ForMember(dest => dest.givenName, src => src.MapFrom(a => a.Properties["givenName"].Value))
            .ForMember(dest => dest.sn, src => src.MapFrom(a => a.Properties["sn"].Value))
            .ForMember(dest => dest.cn, src => src.MapFrom(a => a.Properties["cn"].Value))
            .ForMember(dest => dest.mail, src => src.MapFrom(a => a.Properties["mail"].Value))
            .ForMember(dest => dest.DisplayName, src => src.MapFrom(a => a.Properties["displayName"].Value))
            .ForMember(dest => dest.PhysicalDeliveryOfficeName, src => src.MapFrom(a => a.Properties["physicalDeliveryOfficeName"].Value))
            .ForMember(dest => dest.Description, src => src.MapFrom(a => a.Properties["description"].Value))
            .ForMember(dest => dest.TelephoneNumber, src => src.MapFrom(a => a.Properties["telephoneNumber"].Value))
            .ForMember(dest => dest.IpPhone, src => src.MapFrom(a => a.Properties["ipPhone"].Value))
            .ForMember(dest => dest.StreetAddress, src => src.MapFrom(a => a.Properties["streetAddress"].Value))
            .ForMember(dest => dest.l, src => src.MapFrom(a => a.Properties["l"].Value))
            .ForMember(dest => dest.c, src => src.MapFrom(a => a.Properties["c"].Value))
            .ForMember(dest => dest.st, src => src.MapFrom(a => a.Properties["st"].Value))
            .ForMember(dest => dest.PostalCode, src => src.MapFrom(a => a.Properties["postalCode"].Value))
            .ForMember(dest => dest.ScriptPath, src => src.MapFrom(a => a.Properties["scriptPath"].Value))
            .ForMember(dest => dest.Vinculo, src => src.MapFrom(a => a.Properties["extensionAttribute4"].Value))
            .ForMember(dest => dest.Responsavel, src => src.MapFrom(a => a.Properties["extensionAttribute6"].Value))
            .ForMember(dest => dest.Office, src => src.MapFrom(a => a.Properties["extensionAttribute15"].Value))
            .ForMember(dest => dest.Nascimento, src => src.MapFrom(a => a.Properties["extensionAttribute12"].Value))
             );

            var mapper = config.CreateMapper();

            Usuario usr = mapper.Map<Usuario>(entry);

            /* 
             * memberOf e ProxyAddress só serão adicionados ao usr se tiverem retornado
             * pelo menos um item.
             * 
             * Sem essa lógica os usuário que pertencem a apenas um grupo ou possuem só um
             * proxy levantam uma exceção.
             */
            usr.MemberOf = new List<string>();
            usr.ProxyAddresses = new List<string>();

            if (entry.Properties["memberOf"].Count > 0)
            {
                foreach (string grupo in entry.Properties["memberOf"])
                {
                    usr.MemberOf.Add(grupo);
                }
            }

            if (entry.Properties["proxyAddresses"].Count > 0)
            {
                foreach (string proxy in entry.Properties["proxyAddresses"])
                {
                    usr.ProxyAddresses.Add(proxy);
                }
            }

            return usr;
        }
    }
}
