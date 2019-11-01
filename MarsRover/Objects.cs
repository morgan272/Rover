using System.Text.RegularExpressions;

public class Rover
{
    public char Orientation { get; set; }

    public int HorizontalLocation { get; set; }

    public int VerticalLocation { get; set; }

    public Rover(int HorizontalLocation, int VerticalLocation, char Orientation)
    {
        this.HorizontalLocation = HorizontalLocation;
        this.VerticalLocation = VerticalLocation;
        this.Orientation = Orientation;
    }

    public int[] ExecuteCommand(char command)
    {
        if (command == 'M')
        {
            Move();
        }
        else
        {
            Turn(command);
        }

        return new int[] { HorizontalLocation, VerticalLocation};
    }

    private void Move()
    {
        switch (Orientation)
        {
            case 'N':
                VerticalLocation++;
                break;
            case 'S':
                VerticalLocation--;
                break;
            case 'E':
                HorizontalLocation++;
                break;
            case 'W':
                HorizontalLocation--;
                break;
        }
    }

    private void Turn(char Direction)
    {
        switch (Direction){
            case 'L':
                switch (Orientation)
                {
                    case 'N':
                        Orientation = 'W';
                        break;
                    case 'S':
                        Orientation = 'E';
                        break;
                    case 'E':
                        Orientation = 'N';
                        break;
                    case 'W':
                        Orientation = 'S';
                        break;
                }
                break;
            case 'R':
                switch (Orientation)
                {
                    case 'N':
                        Orientation = 'E';
                        break;
                    case 'S':
                        Orientation = 'W';
                        break;
                    case 'E':
                        Orientation = 'S';
                        break;
                    case 'W':
                        Orientation = 'N';
                        break;
                }
                break;
        }
    }
}

public class Plan
{
    private string Bounds;
    private string StartPosition;
    public string Commands { get; }

    public Plan(string bounds, string startPosition, string commands) 
    {
        Bounds = bounds;
        StartPosition = startPosition;
        Commands = commands;
        Validate();
    }

    public int BoundaryLimit => int.Parse(Bounds.Substring(0, Bounds.Length / 2));

    public int HorizontalStart => int.Parse(StartPosition.Split(" ")[0].Substring(0, 1));

    public int VerticalStart => int.Parse(StartPosition.Split(" ")[0].Substring(1, 1));

    public char StartOrientation => char.Parse(StartPosition.Split(" ")[1]);

    private void Validate()
    {
        //Test boundary is only numbers
        if (!new Regex(@"^[0-9]*$").IsMatch(Bounds)) throw new System.Exception("Invalid boundary specified.");

        //Test boundary is a square
        if (Bounds.Length % 2 != 0 
            || !Bounds.EndsWith(Bounds.Substring(0, Bounds.Length / 2))) 
            throw new System.Exception("Boundary is not a square.");

        //Test Boundary Size > 0
        if (BoundaryLimit == 0) throw new System.Exception("Boundary limit must be greater than 0");

        //Test format of start position
        var arrStartPosition = StartPosition.Split(" ");

        if (arrStartPosition.Length != 2) throw new System.Exception("Invalid starting co-ordinate format.");

        if (!new Regex(@"^[1-9]{2}$").IsMatch(arrStartPosition[0])) 
            throw new System.Exception("Invalid starting co-ordinates.");

        if (!new Regex(@"^[NSEW]{1}$").IsMatch(arrStartPosition[1]))
            throw new System.Exception("Invalid start orientation.");
        
        //Test start position is within boundary.
        if (int.Parse(arrStartPosition[0].Substring(0, 1)) > BoundaryLimit || int.Parse(arrStartPosition[0].Substring(1, 1)) > BoundaryLimit)
            throw new System.Exception("Starting co-ordinates outside of boundary.");

        //Test commands are valid
        if (!new Regex(@"^[RLM]").IsMatch(Commands))
            throw new System.Exception("Invalid commands.");
    }
}


