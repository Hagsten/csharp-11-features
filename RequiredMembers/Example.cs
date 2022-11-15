namespace Demo.RequiredMembers
{
    public class Person
    {
        public required string FirstName { get; init; }
        public int Age  { get; init; }

        public Person(string firstName)
        {
            FirstName = firstName;
        }
    }


    public class Example
    {
        public void Demo()
        {
            var person = new Person("Hej")
            {
                FirstName = "Hej"
            };


        }
    }
}



/*
 *
 

        //[SetsRequiredMembers]
        //public Person(string firstName)
        //{
        //    FirstName = firstName;
        //}
 *
 */