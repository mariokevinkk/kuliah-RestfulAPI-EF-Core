using Microsoft.EntityFrameworkCore;
using SimpleRESTApi.Data;
using SimpleRESTApi.Models;

var builder = WebApplication.CreateBuilder(args);

//add ef core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//Dependency Injection
// builder.Services.AddSingleton<ICategory,CategoryADO>();
builder.Services.AddScoped<ICategory,CategoryEF>();
// builder.Services.AddSingleton<IInstructor,InstructorADO>();
builder.Services.AddScoped<IInstructor,InstructorEF>();
// builder.Services.AddSingleton<ICourse,CourseADO>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/*
//optional parameter
app.MapGet("api/v1/helloservices",(string? id)=>
{
    return $"Hello World id={id}";
});
//https://localhost:7148/api/v1/helloservices/querystring?id=12
//https://localhost:7148/api/v1/helloservices

//dengan parameter-> url segmen
app.MapGet("api/v1/helloservices/{nama}",(string nama)=>$"Hello, from {nama}!");
//https://localhost:7148/api/v1/helloservices/mario

app.MapGet("api/v1/luas-segitiga",(double alas, double tinggi)=>
{
    double luas = 0.5*alas*tinggi;
    return $"Luas = {luas}";
});
// https://localhost:7148/api/v1/luas-segitiga?alas=34&tinggi=12
*/
app.MapGet("api/v1/categories",(ICategory categoryData)=>{
// Category category = new Category();
// category.CategoryId=1;
// category.CategoryName="ASP.NET Core";
// return category;
var categories = categoryData.GetCategories();
return categories;
});

app.MapGet("api/v1/categories/{id}",(ICategory categoryData,int id)=>{
var categories = categoryData.GetCategoryById(id);
return categories;
});

app.MapPost("api/v1/categories",(ICategory categoryData,Category category)=>{
var newCategory = categoryData.AddCategory(category);
return newCategory;
});

app.MapPut("api/v1/categories",(ICategory categoryData,Category category)=>{
var updateCategory = categoryData.UpdateCategory(category);
return updateCategory;
});
app.MapDelete("api/v1/categories/{id}",(ICategory categoryData,int id)=>{
categoryData.DeleteCategory(id);
return Results.NoContent();
});

app.MapGet("api/v1/instructors",(IInstructor instructorData)=>{
var instructors = instructorData.GetInstructors();
return instructors;
});

app.MapGet("api/v1/instructors/{id}",(IInstructor instructorData,int id)=>{
var instructors = instructorData.GetInstructorById(id);
return instructors;
});

app.MapPost("api/v1/instructors",(IInstructor instructorData,Instructor instructor)=>{
var newInstructor = instructorData.AddInstructor(instructor);
return newInstructor;
});

app.MapPut("api/v1/instructors",(IInstructor instructorData,Instructor instructor)=>{
var updateInstructor = instructorData.UpdateInstructor(instructor);
return updateInstructor;
});
app.MapDelete("api/v1/instructors/{id}",(IInstructor instructorData,int id)=>{
instructorData.DeleteInstructor(id);
return Results.NoContent();
});
// app.MapGet("api/v1/courses",(ICourse courseData)=>{
// var courses = courseData.GetCourses();
// return courses;
// });
// app.MapGet("api/v1/courses/{id}",(ICourse courseData,int id)=>{
// var courses = courseData.GetCourseById(id);
// return courses;
// });
// app.MapPost("api/v1/courses",(ICourse courseData,Course course)=>{
// var newCourse = courseData.AddCourse(course);
// return newCourse;
// });
// app.MapPut("api/v1/courses",(ICourse courseData,Course course)=>{
// var updateCourse = courseData.UpdateCourse(course);
// return updateCourse;
// });
// app.MapDelete("api/v1/courses/{id}",(ICourse courseData,int id)=>{
// courseData.DeleteCourse(id);
// return Results.NoContent();
// });

app.Run();

