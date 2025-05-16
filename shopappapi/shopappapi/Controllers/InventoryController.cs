using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using shopappapi.Models;
using ShopAppApi.Models;

namespace ShopAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class InventoryController : ControllerBase
  {
    [HttpPost]
    public IActionResult SaveInventoryData( InventoryRequestDto requestDto)
    {
      var connectionString = "Server=localhost;Database=shopapp;Trusted_Connection=True;Encrypt=False;";

      using var connection = new SqlConnection(connectionString);
      using var command = new SqlCommand("sp_SaveInventoryData", connection)
      {
        CommandType = System.Data.CommandType.StoredProcedure
      };

      command.Parameters.AddWithValue("@ProductId", requestDto.ProductId);
      command.Parameters.AddWithValue("@ProductName", requestDto.ProductName);
      command.Parameters.AddWithValue("@AvailableQty", requestDto.AvailableQty);
      command.Parameters.AddWithValue("@ReOrderPoint", requestDto.ReOrderPoint);

      connection.Open();
      command.ExecuteNonQuery();
      connection.Close();

      return Ok();
    }
    [HttpGet]
    public IActionResult GetInventoryData()
    {
      var connectionString = "Server=localhost;Database=shopapp;Trusted_Connection=True;Encrypt=False;";

      using var connection = new SqlConnection(connectionString);
      using var command = new SqlCommand("sp_GetInventoryData", connection)
      {
        CommandType = System.Data.CommandType.StoredProcedure
      };

      connection.Open();

      List<InventoryDto> response = new List<InventoryDto>();

      using (SqlDataReader sqlDataReader = command.ExecuteReader())
      {
        while (sqlDataReader.Read())
        {
          InventoryDto inventoryDto = new InventoryDto();
          inventoryDto.ProductId = Convert.ToInt32(sqlDataReader["ProductId"]);
          inventoryDto.ProductName = Convert.ToString(sqlDataReader["ProductName"]);
          inventoryDto. AvailableQty = Convert.ToInt32(sqlDataReader["AvailableQty"]);
          inventoryDto.ReOrderPoint = Convert.ToInt32(sqlDataReader["ReOrderPoint"]);

          response.Add(inventoryDto);
        }
      }
        connection.Close();

      return Ok(response);
    }
    [HttpDelete("{ProductId}")]
    public IActionResult DeleteInventoryData(int ProductId)
    {
      var connectionString = "Server=localhost;Database=shopapp;Trusted_Connection=True;Encrypt=False;";

      using var connection = new SqlConnection(connectionString);
      using var command = new SqlCommand("sp_DeleteInventoryData", connection)
      {
        CommandType = System.Data.CommandType.StoredProcedure
      };

      command.Parameters.AddWithValue("@ProductId", ProductId);

      connection.Open();
      command.ExecuteNonQuery();
      connection.Close();

      return Ok();
    }
    [HttpPut]
    public IActionResult UpdateInventoryData(InventoryRequestDto requestDto)
    {
      var connectionString = "Server=localhost;Database=shopapp;Trusted_Connection=True;Encrypt=False;";

      using var connection = new SqlConnection(connectionString);
      using var command = new SqlCommand("sp_UpdateInventoryData", connection)
      {
        CommandType = System.Data.CommandType.StoredProcedure
      };

      command.Parameters.AddWithValue("@ProductId", requestDto.ProductId);
      command.Parameters.AddWithValue("@ProductName", requestDto.ProductName);
      command.Parameters.AddWithValue("@AvailableQty", requestDto.AvailableQty);
      command.Parameters.AddWithValue("@ReOrderPoint", requestDto.ReOrderPoint);

      connection.Open();
      command.ExecuteNonQuery();
      connection.Close();

      return Ok();
    }

  }
}
