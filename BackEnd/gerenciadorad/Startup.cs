//namespace GerenciadoraAD
//{
//    public class Startup
//    {

//        public Startup(IConfiguration configuration) {
//            configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        public void ConfigureServices(IServiceCollection services) { 
//            services.AddControllers().AddFluent
//            services.AddControllers();
//            services.AddEndpointsApiExplorer();
//            services.AddSwaggerGen();
//        }

//        //configurar midleware
//        public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment()) { 
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();
//            app.UseAuthorization();
//        }
//    }
//}
