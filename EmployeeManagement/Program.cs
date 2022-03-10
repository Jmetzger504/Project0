using System;

namespace EmployeeManagement
{
    class Employee
    {
        public int empNo { get; set; }
        public string empName { get; set; }
        public int empSalary { get; set; }
        public bool empIsPermanent {get; set;}

        public double getBonus(double percentage) {

            double bonus = this.empSalary*percentage/100;
            return bonus;

        }
    }

    class Manager : Employee {}

    class Developer : Employee {}

    class HR : Employee {}
    
}
