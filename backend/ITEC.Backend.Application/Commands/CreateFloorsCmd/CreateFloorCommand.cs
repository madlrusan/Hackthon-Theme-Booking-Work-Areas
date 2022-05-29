namespace ITEC.Backend.Application.Commands.CreateFloorsCmd
{
    public class CreateFloorCommand
    {
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public List<CreateDeskCommand> Desks { get; set; }
    }
}