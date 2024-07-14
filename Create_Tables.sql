/* 创建4个基本信息表 */
USE DB_TeachingMS
GO
/* 创建TB_TeachingYear表 */
CREATE TABLE TB_TeachingYear                  
( TeachingYearID CHAR(4) PRIMARY KEY,                                             
  TeachingYearName NCHAR(11) NOT NULL                         
)
/* 创建TB_Term表 */
CREATE TABLE TB_Term                  
( TermID CHAR(2) PRIMARY KEY,                                             
  TermName CHAR(8) NOT NULL                         
) 
/* 创建TB_Title表 */
CREATE TABLE TB_Title                  
( TitleID CHAR(2) PRIMARY KEY,                                             
  TitleName CHAR(8) NOT NULL                         
)
/* 创建TB_Dept表 */
CREATE TABLE TB_Dept                  
( DeptID CHAR(2) PRIMARY KEY,                                             
  DeptName CHAR(20) NOT NULL,                        
  DeptSetDate SMALLDATETIME NOT NULL,
  DeptScript TEXT NOT NULL
)
  
/* 创建6个对象信息表 */
USE DB_TeachingMS
GO
/* 创建TB_Spec表 */
CREATE TABLE TB_Spec                  
( SpecID CHAR(4) PRIMARY KEY,                                             
  SpecName CHAR(20) NOT NULL,                         
  DeptID CHAR(2) NOT NULL REFERENCES TB_Dept(DeptID),
  SpecScript TEXT NOT NULL
)  
/* 创建TB_Teacher表 */
CREATE TABLE TB_Teacher                  
( TeacherID CHAR(6) PRIMARY KEY CHECK (TeacherID LIKE 'T[0-9][0-9][0-9][0-9][0-9]'),                                             
  TeacherName CHAR(8) NOT NULL,                         
  DeptID CHAR(2) NOT NULL REFERENCES TB_Dept(DeptID),
  Sex CHAR(1) NOT NULL DEFAULT('M') CHECK (Sex IN ('M','F')),  
  Birthday SMALLDATETIME NOT NULL,
  TPassword VARCHAR(32) NOT NULL DEFAULT('123456'),
  TitleID CHAR(2) NOT NULL REFERENCES TB_Title(TitleID)
)
/* 创建TB_Class表 */
CREATE TABLE TB_Class                  
( ClassID CHAR(6) PRIMARY KEY,                                             
  ClassName CHAR(20) NOT NULL,                         
  DeptID CHAR(2) NOT NULL REFERENCES TB_Dept(DeptID),
  TeacherID CHAR(6) NOT NULL REFERENCES TB_Teacher(TeacherID)
)
/* 创建TB_Student表 */
CREATE TABLE TB_Student                  
( StuID CHAR(8) PRIMARY KEY CHECK (StuID LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),                                             
  StuName CHAR(8) NOT NULL,                         
  EnrollYear CHAR(4) NOT NULL CHECK (EnrollYear LIKE '[0-9][0-9][0-9][0-9]'),
  GradYear CHAR(4) NOT NULL CHECK (GradYear LIKE '[0-9][0-9][0-9][0-9]'),
  DeptID CHAR(2) NOT NULL REFERENCES TB_Dept(DeptID),
  ClassID CHAR(6) NOT NULL REFERENCES TB_Class(ClassID),
  Sex CHAR(1) NOT NULL DEFAULT('M') CHECK (Sex IN ('M','F')),
  Birthday SMALLDATETIME NOT NULL,
  SPassword VARCHAR(32) NOT NULL DEFAULT('123456'),
  StuAddress VARCHAR(64) NOT NULL,
  ZipCode CHAR(6) NOT NULL CHECK (ZipCode LIKE '[0-9][0-9][0-9][0-9][0-9][0-9]') 
)    
/* 创建TB_Course表 */
CREATE TABLE TB_Course                  
( CourseID CHAR(6) PRIMARY KEY CHECK (CourseID LIKE 'C[0-9][0-9][0-9][0-9][0-9]'),                                             
  CourseName VARCHAR(32) NOT NULL UNIQUE,                         
  DeptID CHAR(2) NOT NULL REFERENCES TB_Dept(DeptID),
  CourseGrade REAL NOT NULL DEFAULT(0) CHECK (CourseGrade>=0),
  LessonTime SMALLINT NOT NULL DEFAULT(0) CHECK (LessonTime>=0),
  CourseOutline TEXT
)     

/* 创建3个业务信息表 */
USE DB_TeachingMS
GO
/* 创建TB_CourseClass表 */
CREATE TABLE TB_CourseClass                  
( CourseClassID CHAR(10) PRIMARY KEY CHECK (CourseClassID LIKE 'T[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),                                             
  CourseID CHAR(6) NOT NULL REFERENCES TB_Course(CourseID),                         
  TeacherID CHAR(6) NOT NULL REFERENCES TB_Teacher(TeacherID),
  TeachingYearID CHAR(4) NOT NULL REFERENCES TB_TeachingYear(TeachingYearID),
  TermID CHAR(2) NOT NULL REFERENCES TB_Term(TermID),
  TeachingPlace NVARCHAR(16) NOT NULL,
  TeachingTime NVARCHAR(32) NOT NULL,  
  CommonPart TINYINT NOT NULL DEFAULT(10) CHECK (CommonPart>=0),
  MiddlePart TINYINT NOT NULL DEFAULT(20) CHECK (MiddlePart>=0),
  LastPart TINYINT NOT NULL DEFAULT(70) CHECK (LastPart>=0),
  MaxNumber SMALLINT NOT NULL DEFAULT(60) CHECK (MaxNumber>=0),
  SelectedNumber SMALLINT NOT NULL DEFAULT(0),
  FullFlag CHAR(1) NOT NULL DEFAULT('U') CHECK (FullFlag IN ('F','U')),
  CONSTRAINT CK_SumOfParts CHECK (CommonPart+MiddlePart+LastPart=100) 
)  
/* 创建TB_SelectCourse表 */
CREATE TABLE TB_SelectCourse                 
( StuID CHAR(8) NOT NULL REFERENCES TB_Student(StuID) ON DELETE CASCADE ON UPDATE CASCADE,
  CourseClassID CHAR(10) NOT NULL REFERENCES TB_CourseClass(CourseClassID) ON DELETE CASCADE ON UPDATE CASCADE,
  SelectDate SMALLDATETIME NOT NULL DEFAULT GETDATE(),
  CONSTRAINT PK_StuID_CourseClassID PRIMARY KEY (StuID, CourseClassID)
)
/* 创建TB_Grade表 */
CREATE TABLE TB_Grade                  
( GradeSeedID INT IDENTITY(1,1) PRIMARY KEY,                                             
  StuID CHAR(8) NOT NULL REFERENCES TB_Student(StuID),
  ClassID CHAR(6) NOT NULL REFERENCES TB_Class(ClassID),
  CourseClassID CHAR(10) NOT NULL REFERENCES TB_CourseClass(CourseClassID),
  CourseID CHAR(6) NOT NULL REFERENCES TB_Course(CourseID),
  CommonScore REAL NOT NULL DEFAULT(0) CHECK (CommonScore>=0 AND CommonScore<=100),
  MiddleScore REAL NOT NULL DEFAULT(0) CHECK (MiddleScore>=0 AND MiddleScore<=100),
  LastScore REAL NOT NULL DEFAULT(0) CHECK (LastScore>=0 AND LastScore<=100),
  TotalScore REAL NOT NULL DEFAULT(0) CHECK (TotalScore>=0 AND TotalScore<=100),
  RetestScore REAL DEFAULT(0) CHECK (RetestScore>=0 AND RetestScore<=100),
  LockFlag CHAR(1) NOT NULL DEFAULT('U') CHECK (LockFlag IN ('U','L'))
)  

