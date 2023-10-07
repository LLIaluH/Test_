SELECT D.Name, 
t1.EmpCount AS 'Кол-во сотрудников в отделе',
t2.TaskCount AS 'Кол-во задач на сотрудников отдела',
t2.SumLaborIntensity AS 'Общая трудоемкость задач отдела',
t3.SumLaborIntensityImportantTasks AS 'Трудоемкость важных задач отдела'
FROM Department AS D
JOIN (
	Select D.Id AS DepartmentId, COUNT(Em.Id) AS EmpCount
	FROM Employes AS Em 
	JOIN Department AS D ON Em.DepartmentId = D.Id
	GROUP BY D.Id) AS t1 ON t1.DepartmentId = D.Id
JOIN (
	Select D.Id AS DepartmentId, COUNT(Et.Id) AS TaskCount, SUM(Et.LaborIntensity) AS SumLaborIntensity
	FROM Employes AS Em 
	JOIN Department AS D ON Em.DepartmentId = D.Id
	JOIN EmployeeTasks AS Et ON Em.Id = Et.EmployeId
	GROUP BY D.Id) AS t2 ON t2.DepartmentId = D.Id
JOIN (
	Select D.Id AS DepartmentId, SUM(Et.LaborIntensity) AS SumLaborIntensityImportantTasks
	FROM Employes AS Em 
	JOIN Department AS D ON Em.DepartmentId = D.Id
	JOIN EmployeeTasks AS Et ON Em.Id = Et.EmployeId
	WHERE Et.ImportantTask = 'Y'
	GROUP BY D.Id) AS t3 ON t3.DepartmentId = D.Id