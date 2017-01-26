namespace Testbed.AutoMapper
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using global::AutoMapper;

	//https://github.com/AutoMapper/AutoMapper/wiki
	class Program
    {
        #region Main Program Loop

        private static ManualResetEvent _quitEvent = new ManualResetEvent(false);

        [STAThread]
        private static void Main(string[] args)
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                _quitEvent.Set();
                e.Cancel = true;
            };

            try
            {
				#region Setup
				#endregion

				InitializeAutoMapper();

				Mapper.AssertConfigurationIsValid();

				ProgramBody();

                //  One of the following should be commented out. The other should be uncommented.

                //_quitEvent.WaitOne();  //  Wait on UI thread for Ctrl + C

                Console.ReadKey(true);  //  Wait for any character input
            }
            finally
            {
                #region Tear down
                #endregion
            }
        }

        #endregion





		private static void InitializeAutoMapper()
		{
			Mapper.Initialize(cfg => 
			{
				cfg.CreateMap<DateTime, string>()
					.ConvertUsing(x => x.ToUniversalTime().ToString("yyyy-MM-ddThh:mm:ssZ"));

				cfg.CreateMap<MyDto, MyModel>()
					.ForMember(x => x.Name, opt => opt.NullSubstitute("Anonymous"));
			});
		}




       
        private static void ProgramBody()
        {


			var x = new MyDto
			{
				Name = "Zack",
				Number = 42,
				Date = DateTime.Parse("2015-10-05T20:00:00Z"),
			};

			var y = Mapper.Map<MyModel>(x);

			Console.WriteLine(y.Name);
			Console.WriteLine(y.Date);
		}
    }

	public class MyDto
	{
		public string Name { get; set; }

		public int Number { get; set; }

		public DateTime Date { get; set; }
	}

	public class MyModel
	{
		public string Name { get; set; }

		public int Number { get; set; }

		public string Date { get; set; }
	}
}
