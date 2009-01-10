namespace TestService 
{
    
    public interface ITestStoreAdapter
    {
        void Fill(TestStore ds);
        void Update(TestStore ds);
    }

    partial class TestStore
    {
        partial class TestRevisionsDataTable
        {
        }
    }
}
