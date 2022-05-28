namespace ITEC.Backend.Application.Commands.CreateFloorsCmd
{
    public class CreateFloorCommand
    {
        public string Name { get; set; }
        public List<CreateDeskCommand> Desks { get; set; }
    }
}