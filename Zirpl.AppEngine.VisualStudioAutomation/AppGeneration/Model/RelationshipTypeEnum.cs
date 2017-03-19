namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Model
{
    public enum RelationshipTypeEnum : byte
    {
        None = 0,
        //OneToZeroOrOne = 1,
        OneToMany = 2,
        //ZeroOrOneToOne = 3,
        //ZeroOrOneToMany = 4,
        ManyToOne = 5,
        //ManyToZeroOrOne = 6,
        ManyToMany = 7
    }
}
