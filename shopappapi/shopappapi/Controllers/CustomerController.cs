using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ShopAppApi.Models;
using System;
using System.Collections.Generic;

namespace ShopAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController : ControllerBase
  {
    private readonly string connectionString = "Server=localhost;Database=shopapp;Trusted_Connection=True;Encrypt=False;";

    [HttpPost]
    public IActionResult AddCustomer(CustomerRequestDto requestDto)
    {
      using var connection = new SqlConnection(connectionString);
      using var command = new SqlCommand("AddCustomer", connection)
      {
        CommandType = System.Data.CommandType.StoredProcedure
      };

      command.Parameters.AddWithValue("@CustomerName", requestDto.CustomerName);
      command.Parameters.AddWithValue("@Phone", requestDto.Phone);
      command.Parameters.AddWithValue("@RegistrationDate", requestDto.RegistrationDate);

      connection.Open();
      command.ExecuteNonQuery();
      connection.Close();

      return Ok();
    }

    [HttpGet]
    public IActionResult GetAllCustomers()
    {
      using var connection = new SqlConnection(connectionString);
      using var command = new SqlCommand("GetAllCustomers", connection)
      {
        CommandType = System.Data.CommandType.StoredProcedure
      };

      connection.Open();

      List<CustomerDto> response = new List<CustomerDto>();

      using (var reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          response.Add(new CustomerDto
          {
            CustomerId = Convert.ToInt32(reader["CustomerId"]),
            CustomerName = Convert.ToString(reader["CustomerName"]),
            Phone = Convert.ToString(reader["Phone"]),
            RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"])
          });
        }
      }

      connection.Close();

      return Ok(response);
    }

    [HttpPut]
    public IActionResult UpdateCustomer(CustomerRequestDto requestDto)
    {
      using var connection = new SqlConnection(connectionString);
      using var command = new SqlCommand("UpdateCustomer", connection)
      {
        CommandType = System.Data.CommandType.StoredProcedure
      };

      command.Parameters.AddWithValue("@CustomerId", requestDto.CustomerId);
      command.Parameters.AddWithValue("@CustomerName", requestDto.CustomerName);
      command.Parameters.AddWithValue("@Phone", requestDto.Phone);
      command.Parameters.AddWithValue("@RegistrationDate", requestDto.RegistrationDate);

      connection.Open();
      command.ExecuteNonQuery();
      connection.Close();

      return Ok();
    }

    [HttpDelete("{customerId}")]
    public IActionResult DeleteCustomer(int customerId)
    {
      using var connection = new SqlConnection(connectionString);
      using var command = new SqlCommand("DeleteCustomer", connection)
      {
        CommandType = System.Data.CommandType.StoredProcedure
      };

      command.Parameters.AddWithValue("@CustomerId", customerId);

      connection.Open();
      command.ExecuteNonQuery();
      connection.Close();

      return Ok();
    }
  }
}
