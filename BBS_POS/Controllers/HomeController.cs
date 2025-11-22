using BBS_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBS_POS.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                // Total Sales
                ViewBag.TotalSales = _context.tbl_sale.Sum(s => (decimal?)s.total) ?? 0;

                // Total Stock
                ViewBag.TotalStock = _context.tbl_quantity.Sum(q => (int?)q.box_quantity) ?? 0;

                // Low stock
                var lowStockProducts = _context.tbl_product
                    .Include(p => p.Quantities)
                    .Select(p => p.Quantities != null ? p.Quantities.Sum(q => (int?)q.box_quantity) : 0)
                    .Count(total => total < 5);

                // Fetch products with Sales and Quantities
                var productsWithData = _context.tbl_product
                    .Include(p => p.Sales)
                    .Include(p => p.Quantities)
                    .ToList();

                // Top 4 products by total sold
                var topProducts = productsWithData
                    .Select(p => new
                    {
                        p.p_id,
                        p.p_name,
                        p.p_buy_price,
                        p.p_sale_price,
                        Quantity = p.Quantities?.Sum(q => q.box_quantity) ?? 0,
                        TotalSold = p.Sales?.Sum(s => (s.qty_box ?? 0) + (s.qty_pieces ?? 0)) ?? 0
                    })
                    .OrderByDescending(p => p.TotalSold)
                    .Take(4)
                    .ToList();

                ViewBag.TopProducts = topProducts;

                // Total Profit
                var totalProfit = _context.tbl_sale
                    .Join(
                        _context.tbl_product,
                        sale => sale.p_id,
                        prod => prod.p_id,
                        (sale, prod) => new
                        {
                            ProfitPerSale = (sale.price - prod.p_buy_price) *
                                            ((sale.qty_box ?? 0) +
                                             (sale.qty_pieces ?? 0) / (prod.p_piece_size > 0 ? prod.p_piece_size : 1))
                        })
                    .Sum(x => (decimal?)x.ProfitPerSale) ?? 0;

                ViewBag.TotalProfit = totalProfit;

                return View();
            }
            catch
            {
                TempData["Ex"] = "Something went wrong!";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                return View();
            }
            catch
            {
                TempData["Ex"] = "Something went wrong!";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Login(string txtname, string txtpwd)
        {
            try
            {
                var user = _context.tbl_user.FirstOrDefault(u => u.u_usr == txtname && u.u_pwd == txtpwd);

                if (user == null)
                {
                    TempData["LoginError"] = "Invalid username or password!";
                    return RedirectToAction("Login");
                }

                HttpContext.Session.SetInt32("u_id", user.u_id);
                TempData["LoginSuccess"] = "Login successful!";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Ex"] = "Something went wrong!";
                return RedirectToAction("Login");
            }
        }

        public IActionResult POS()
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("u_id");
                if (userId == null)
                {
                    TempData["LoginError"] = "Please login first!";
                    return RedirectToAction("Login");
                }

                List<Product> products = _context.tbl_product.ToList();
                return View(products);
            }
            catch
            {
                TempData["Ex"] = "Something went wrong!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult SaveSale([FromBody] List<Sale> sales)
        {
            try
            {
                if (sales == null || !sales.Any())
                {
                    return BadRequest("No sale data received.");
                }

                foreach (var sale in sales)
                {
                    sale.s_date = DateTime.Now;
                    _context.tbl_sale.Add(sale);
                }

                _context.SaveChanges();
                return Ok(new { message = "Sales saved successfully." });
            }
            catch
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("u_id");
                if (userId == null)
                    return RedirectToAction("Login");

                return View(_context.tbl_product.ToList());
            }
            catch
            {
                TempData["Ex"] = "Something went wrong!";
                return View();
            }
        }

        [HttpPost]
        public IActionResult AddProduct(Product model)
        {
            try
            {
                model.p_created_at = DateTime.Now;
                _context.tbl_product.Add(model);
                _context.SaveChanges();

                TempData["success"] = "Product added successfully!";
                return RedirectToAction("AddProduct");
            }
            catch
            {
                TempData["Ex"] = "Something went wrong!";
                return RedirectToAction("AddProduct");
            }
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            try
            {
                if (product == null || product.p_id <= 0)
                {
                    TempData["Ex"] = "Invalid product data!";
                    return RedirectToAction("AddProduct");
                }

                var data = _context.tbl_product.Find(product.p_id);
                if (data != null)
                {
                    data.p_name = product.p_name;
                    data.p_buy_price = product.p_buy_price;
                    data.p_sale_price = product.p_sale_price;
                    data.p_box_size = product.p_box_size;
                    data.p_piece_size = product.p_piece_size;
                    data.p_updated_at = DateTime.Now;

                    _context.SaveChanges();
                    TempData["UpdateProduct"] = "Product updated successfully!";
                }
                else
                {
                    TempData["Ex"] = "Product not found!";
                }

                return RedirectToAction("AddProduct");
            }
            catch
            {
                TempData["Ex"] = "Something went wrong!";
                return RedirectToAction("AddProduct");
            }
        }

        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var _id = _context.tbl_product.FirstOrDefault(row => row.p_id == id);
                var q_id = _context.tbl_quantity.FirstOrDefault(row => row.p_id == id);

                if (_id == null || q_id == null)
                {
                    TempData["Ex"] = "Product/Quantity not found!";
                    return RedirectToAction("AddProduct");
                }

                _context.tbl_product.Remove(_id);
                _context.tbl_quantity.Remove(q_id);
                _context.SaveChanges();
                TempData["DeleteProduct"] = "Product/Quantity deleted successfully!";
                return RedirectToAction("AddProduct");
            }
            catch
            {
                TempData["Ex"] = "Something went wrong!";
                return RedirectToAction("AddProduct");
            }
        }

        public IActionResult AddQuantity()
        {
            try
            {
                ViewBag.Products = _context.tbl_product.ToList();
                var quantities = _context.tbl_quantity
                    .Select(q => new { q.q_id, q.p_id, ProductName = q.Product.p_name, q.box_quantity })
                    .ToList();
                ViewBag.Quantities = quantities;
                return View();
            }
            catch
            {
                TempData["Ex"] = "Something went wrong!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult AddQuantity(Quantity quantity)
        {
            try
            {
                _context.tbl_quantity.Add(quantity);
                _context.SaveChanges();

                TempData["Success"] = "Quantity added successfully!";

                ViewBag.Products = _context.tbl_product.ToList();
                var quantities = _context.tbl_quantity
                    .Select(q => new { q.q_id, q.p_id, ProductName = q.Product.p_name, q.box_quantity })
                    .ToList();
                ViewBag.Quantities = quantities;

                return View(quantity);
            }
            catch
            {
                TempData["Ex"] = "Something went wrong!";
                return RedirectToAction("AddQuantity");
            }
        }

        public IActionResult Sales()
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("u_id");
                if (userId == null)
                    return RedirectToAction("Login");

                var sales = _context.tbl_sale
                                    .OrderByDescending(x => x.s_date)
                                    .ToList();

                return View(sales);
            }
            catch
            {
                TempData["Ex"] = "Something went wrong!";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Stock()
        {
            try
            {
                var stockData = _context.tbl_product
                    .Select(p => new
                    {
                        p.p_id,
                        p.p_name,
                        BoxSize = p.p_box_size,
                        PieceSize = p.p_piece_size == 0 ? 0 : p.p_piece_size,
                        TotalBoxes = p.Quantities.Sum(q => (int?)q.box_quantity) ?? 0,
                        TotalSoldBoxes = p.Sales.Sum(s => (int?)s.qty_box) ?? 0,
                        TotalSoldPieces = p.Sales.Sum(s => (int?)s.qty_pieces) ?? 0
                    })
                    .ToList()
                    .Select(p =>
                    {
                        if (p.PieceSize == 0)
                        {
                            return new
                            {
                                p.p_id,
                                p.p_name,
                                p.BoxSize,
                                p.PieceSize,
                                TotalBoxes = p.TotalBoxes,
                                TotalSoldBoxes = p.TotalSoldBoxes,
                                TotalSoldPieces = p.TotalSoldPieces,
                                RemainingBoxes = p.TotalBoxes - p.TotalSoldBoxes,
                                RemainingPieces = 0
                            };
                        }

                        int totalPieces = p.TotalBoxes * p.PieceSize;
                        int soldPiecesFromBoxes = p.TotalSoldBoxes * p.PieceSize;
                        int totalSoldPieces = soldPiecesFromBoxes + p.TotalSoldPieces;

                        int remainingPieces = totalPieces - totalSoldPieces;
                        if (remainingPieces < 0) remainingPieces = 0;

                        int remainingBoxes = remainingPieces / p.PieceSize;
                        int loosePieces = remainingPieces % p.PieceSize;

                        return new
                        {
                            p.p_id,
                            p.p_name,
                            p.BoxSize,
                            p.PieceSize,
                            TotalBoxes = p.TotalBoxes,
                            TotalSoldBoxes = p.TotalSoldBoxes,
                            TotalSoldPieces = p.TotalSoldPieces,
                            RemainingBoxes = remainingBoxes,
                            RemainingPieces = loosePieces
                        };
                    })
                    .ToList();

                ViewBag.Stock = stockData;
                return View();
            }
            catch
            {
                TempData["Ex"] = "Something went wrong!";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Remove("u_id");
                HttpContext.Session.Clear();
                TempData["Logout"] = "Logout Successful!";
                return RedirectToAction("Login");
            }
            catch
            {
                TempData["Ex"] = "Something went wrong!";
                return RedirectToAction("Login");
            }
        }
    }
}