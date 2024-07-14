SELECT TB_Dept.DeptID,
       TB_Dept.DeptName,
       TB_Class.ClassID,
       TB_Class.ClassName,
       AVG(TB_Grade.TotalScore) AS avg
FROM TB_Class
JOIN TB_Dept ON TB_Class.DeptID = TB_Dept.DeptID
LEFT JOIN TB_Student ON TB_Class.ClassID = TB_Student.ClassID
LEFT JOIN TB_Grade ON TB_Student.StuID = TB_Grade.StuID
GROUP BY TB_Dept.DeptID, TB_Dept.DeptName, TB_Class.ClassID, TB_Class.ClassName
ORDER BY TB_Dept.DeptID ASC, TB_Class.ClassID ASC;
