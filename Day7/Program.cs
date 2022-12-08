using System.Collections;
using System.Linq;

namespace FileSystem
{
    class Program
    {
        class Directory
        {
            public Directory()
            {
                Name = "/";
                Directories = new List<Directory>();
                Files = new Dictionary<string, int>();    
            }

            public Directory(string name, Directory parent)
            {
                Name = name;
                Parent = parent;
                Directories = new List<Directory>();
                Files = new Dictionary<string, int>();    
            }

            public string Name {get; set;}
            public Directory? Parent {get; set;}
            public List<Directory> Directories {get; set;}
            public Dictionary<string, int> Files {get; set;}

            public int Size { get {return DirectorySize(this);} }

            int DirectorySize(Directory directory)
            {
                var directorySize = directory.Files.Sum(d => d.Value);
                
                foreach (var dir in directory.Directories)
                {
                    var size = DirectorySize(dir);
                    directorySize += size;
                }
                
                return directorySize;
            }
        }

        static int DirectorySize(Directory directory)
        {
            var directorySize = directory.Files.Sum(d => d.Value);
            
            foreach (var dir in directory.Directories)
            {
                var size = DirectorySize(dir);
                directorySize += size;
            }
            
            return directorySize;
        }

        static void Main(string[] args)
        {
            var currentDir = new Directory();
            var directories = new List<Directory>();

            foreach (var line in System.IO.File.ReadLines(@"filesystem.txt"))
            {
                if (line.StartsWith("$"))
                {
                    if (line.Contains("$ cd"))
                    {
                        var action = line.Substring(5);
                        switch (action)
                        {
                            case "/":
                                var dir = new Directory();
                                currentDir = dir;
                                directories.Add(dir);
                                break;

                            case "..":
                                currentDir = currentDir?.Parent;
                                break;

                            default:
                                dir = currentDir?.Directories.Single(d => d.Name == action); 
                                currentDir = dir;
                                break;
                        }
                    }
                }
                else
                {
                    if (line.Contains("dir "))
                    {
                        var directoryName = line.Substring(4);
                        var newDir = new Directory(directoryName, currentDir);
                        directories.Add(newDir);
                        currentDir.Directories.Add(newDir);
                    }
                    else
                    {
                        var fileInfo = line.Split(" ");
                        var fileSize = int.Parse(fileInfo[0]);
                        var fileName = fileInfo[1];
                        currentDir?.Files.Add(fileName, fileSize);
                    }
                }
            }

            var totalSizeLessThan100K = 0;
            foreach (var dir in directories)
            {
                var directorySize = DirectorySize(dir);
                totalSizeLessThan100K += directorySize <= 100000 ? directorySize : 0;

                Console.WriteLine($"Directory name: {dir.Name} Parent: {dir.Parent?.Name} " +
                $"File count: {dir.Files.Count} Directory count {dir.Directories.Count} Directory size: {directorySize}");

                Console.WriteLine($"Sum of directories <= 100,000: {totalSizeLessThan100K}");
            }

            var totalDiskSpace = 70000000;
            var requiredFreeSpace = 30000000;
            var totalUsedSpace = DirectorySize(directories[0]);
            var totalUnusedSpace = totalDiskSpace - totalUsedSpace;

            // 358913
            var deletionSize = requiredFreeSpace - totalUnusedSpace;
            var orderedDirectories = directories.OrderBy(d => d.Size);

            // 366028
            var smallestToDelete = orderedDirectories.First(od => od.Size >= deletionSize);
            Console.WriteLine($"Smallest directory to delete: {smallestToDelete.Name} - {smallestToDelete.Size}");
        }
    }
}