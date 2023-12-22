namespace day_20;

abstract class Module
{
    public const bool HIGH = true;
    public const bool LOW = false;

    // %nr -> hf, mt
    // broadcaster -> tl, gd, zb, gc
    // &qz -> qn
    public static Module Parse(string line)
    {
        var parts = line.Split("->");
        var name = parts[0].Trim();
        var outputs = parts[1].Split(",").Select(s => s.Trim()).ToArray();

        Module module =
            name.StartsWith('%') ? new FlipFlopModule()
            : name.StartsWith('&') ? new ConjunctionModule()
            : name.Equals("broadcaster") ? new BroadcastModule()
            : throw new Exception($"Unknown module type: {name}");

        module.Name = name.TrimStart(['%', '&']);
        module.Destinations.AddRange(outputs);
        return module;
    }
    public string Name { get; private set; }

    public List<string> Destinations { get; private set; } = [];

    public abstract bool? Receive(bool pulse, string? from = null);

    public override string ToString()
    {
        return $"{GetType()} {Name} -> {string.Join(", ", Destinations)}";
    }
}

class FlipFlopModule : Module
{
    private bool state = false;

    public override bool? Receive(bool pulse, string? from = null)
    {
        if (pulse == HIGH) return null;
        state = !state;
        return state;
    }
}

class ConjunctionModule : Module
{
    public Dictionary<string, bool> mostRecent = new Dictionary<string, bool>();
    public void AddInput(string name)
    {
        mostRecent[name] = LOW;
    }

    public override bool? Receive(bool pulse, string? from = null)
    {
        mostRecent[from!] = pulse;
        return mostRecent.Values.All(v => v == HIGH) ? LOW : HIGH;
    }

}

class BroadcastModule : Module
{
    public override bool? Receive(bool pulse, string? from = null)
    {
        return pulse;
    }
}


public class Puzzle
{

    private readonly string input;
    public Puzzle(string input)
    {
        this.input = input;
    }

    public int Part1()
    {
        Dictionary<string, Module> modules = [];

        input.Trim().Split("\n").Select(Module.Parse).ToList().ForEach(m => modules[m.Name] = m);

        // Add the inputs for the conjunction modules
        foreach (var module in modules.Values.OfType<ConjunctionModule>())
        {
            foreach (var source in modules.Values)
            {
                if (source.Destinations.Contains(module.Name))
                {
                    module.AddInput(source.Name);
                }
            }
        }

        int lowCount = 0;
        int highCount = 0;

        for (int i = 0; i < 1000; i++)
        {
            var queue = new Queue<(string, string, bool)>();
            queue.Enqueue(("button", "broadcaster", Module.LOW));
            while (queue.Count > 0)
            {
                var (from, to, pulse) = queue.Dequeue();

                if (pulse == Module.LOW) lowCount++;
                else highCount++;

                if (!modules.ContainsKey(to)) continue;

                var module = modules[to];
                var result = module.Receive(pulse, from);
                if (result.HasValue)
                {
                    foreach (var destination in module.Destinations)
                    {
                        queue.Enqueue((to, destination, result.Value));
                    }
                }
            }
        }

        return lowCount * highCount;
    }

    public long Part2()
    {
        Dictionary<string, Module> modules = [];

        input.Trim().Split("\n").Select(Module.Parse).ToList().ForEach(m => modules[m.Name] = m);

        // Add the inputs for the conjunction modules
        foreach (var module in modules.Values.OfType<ConjunctionModule>())
        {
            foreach (var source in modules.Values)
            {
                if (source.Destinations.Contains(module.Name))
                {
                    module.AddInput(source.Name);
                }
            }
        }
        long buttonCount = 0;

        while (true)
        {
            buttonCount++;
            int rxLowCount = 0;
            int rxHighCount = 0;

            var queue = new Queue<(string, string, bool)>();
            queue.Enqueue(("button", "broadcaster", Module.LOW));
            while (queue.Count > 0)
            {
                var (from, to, pulse) = queue.Dequeue();

                if (!modules.ContainsKey(to)) continue;

                var module = modules[to];

                if (module.Name == "qn")
                {

                    var cm = module as ConjunctionModule;
                    if (cm.mostRecent.Any(x => x.Value == Module.HIGH))
                    {
                        Console.WriteLine($"{buttonCount,10})");
                        foreach (var (name, value) in cm.mostRecent)
                        {
                            Console.WriteLine($"{name}: {value}");
                        }
                    }
                }

                if (module.Name == "rx")
                {
                    if (pulse == Module.LOW) rxLowCount++;
                    else rxHighCount++;
                }

                var result = module.Receive(pulse, from);
                if (result.HasValue)
                {
                    foreach (var destination in module.Destinations)
                    {
                        queue.Enqueue((to, destination, result.Value));
                    }
                }
            }

            // if (buttonCount % 100_000 == 0)
            // {
            //     Console.WriteLine($"{buttonCount,10}) RX: {rxLowCount,4} {rxHighCount,4}");
            // }
            if (rxLowCount != 0 || rxHighCount != 0)
            {
                Console.WriteLine($"{buttonCount,10}) RX: {rxLowCount,4} {rxHighCount,4}");
            }
            if (rxLowCount == 1 && rxHighCount == 0)
            {
                return buttonCount;
            }

        }
    }
}
