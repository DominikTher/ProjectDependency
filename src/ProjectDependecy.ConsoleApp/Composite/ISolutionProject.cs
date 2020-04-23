namespace ProjectDependecy.ConsoleApp.Composite
{
    interface ISolutionProject
    {
        string Name { get; }
        void Add(ISolutionProject project);
        bool IsComposite();
        bool IsRoot();
    }
}
