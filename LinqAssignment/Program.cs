using System;
using System.Linq;
using System.Collections.Generic;
namespace LinqAssignment{					
public class Program
{
	IList<Employee> employeeList;
	IList<Salary> salaryList;
	
	public Program(){
		employeeList = new List<Employee>() { 
			new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
			new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
			new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
			new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
			new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
			new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
			new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}			
		};
		
		salaryList = new List<Salary>() {
			new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
		};
	}
	
	public static void Main()
	{		
		Program program = new Program();
		
		//program.Task1();
		
		//program.Task2();
		
		program.Task3();
	}
	
	public void Task1(){
		//Print total Salary of all the employees with 
        //their corresponding names in ascending order of their salary.
        var results = employeeList.GroupJoin(
                                    salaryList,
                                    salary => salary.EmployeeID,
                                    employee => employee.EmployeeID,
                                    (employee, salary) => new{
                                        totalSalary = salaryList.Where(x => x.EmployeeID == employee.EmployeeID).Sum(x=>x.Amount),
                                        name = employee.EmployeeFirstName + " " + employee.EmployeeLastName
                                    } 
                                    );
        foreach (var result in results.OrderBy(x => x.totalSalary)){
            System.Console.WriteLine("Name: {0}",result.name);
            System.Console.WriteLine("Salary: {0}",result.totalSalary);
        }
        
	}
	
	public void Task2(){
		 //Print Employee details of 2nd oldest employee 
         //including his/her total monthly salary.
		 var result = employeeList.OrderByDescending(x => x.Age).Skip(1).Take(1).Single();
		 var totalSalary = salaryList.Where(x => x.EmployeeID == result.EmployeeID ).Sum(x=>x.Amount);
		 Console.WriteLine("Name :- {0}",result.EmployeeFirstName+" "+result.EmployeeLastName);
		 Console.WriteLine("Age :- {0}",result.Age);
		 Console.WriteLine("Salary:- {0}",totalSalary);
	}
	
	public void Task3(){
		 //Print means of Monthly, Performance, Bonus salary of employees 
		 //whose age is greater than 30.
		 var result = salaryList.GroupBy(x => x.Type).ToList();
		 foreach(var k in result){
			 System.Console.WriteLine("Salary Type:- {0}",k.Key);
			 var res = k.Join(employeeList,
						_k => _k.EmployeeID,
						 _z=> _z.EmployeeID,
						(s,p)=>new { 
							Age = p.Age,
							Salary = s.Amount
						}
			);
			var means = res.Where(x => x.Age > 30).Sum(x => x.Salary);
			System.Console.WriteLine("Mean : {0}", means);
		 }

	}
}

public enum SalaryType{
	Monthly,
	Performance,
	Bonus
}

public class Employee{
	public int EmployeeID { get; set; }
	public string EmployeeFirstName { get; set; }
	public string EmployeeLastName { get; set; }
	public int Age { get; set; }	
}

public class Salary{
	public int EmployeeID { get; set; }
	public int Amount { get; set; }
	public SalaryType Type { get; set; }
}
}