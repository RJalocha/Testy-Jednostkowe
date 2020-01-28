const { Builder, By, Key, until } = require("selenium-webdriver");
var assert = require('assert');
const driver = new Builder().forBrowser("chrome").build();
const timeoutInSeconds = 10;
// driver.manage().timeouts().pageLoadTimeout(60, TimeUnit.SECONDS);
jest.setTimeout(timeoutInSeconds * 1000);

//beforeEach(async () => {
//  driver = await  new Builder().forBrowser("chrome").build();
//})
// 
// afterEach(async () => {
//   await driver.quit();
// })

async function waitForPageToLoad(elemId, _selector) {
  const selector = _selector || By.id;
  const timeout = timeoutInSeconds * 1000;
  return await driver.wait(
    function() {
      return driver.findElement(selector(elemId)).then(
        () => true,
        () => false
      );
    },
    timeout,
    "Expectation error: Timed out waiting for current url"
  );
}



test("Home Page - Loads", async function () {
  await driver.get("http://localhost:57541/");
  await waitForPageToLoad("jumbotron-order-service")
});

test("Home Page - ClientOrder Link Works", async function () {
  await driver.get("http://localhost:57541/");
  await waitForPageToLoad("jumbotron-order-service")

  await driver
    .findElement(By.id("btn-show-all-client-orders"))
    .click();
  await waitForPageToLoad("client-order-list-header");
});

test("Home Page - Products Link Works", async function () {
  await driver.get("http://localhost:57541/");
  await waitForPageToLoad("jumbotron-order-service")

  await driver
    .findElement(By.id("btn-show-all-products"))
    .click();
  await waitForPageToLoad("products-list-header");
});

test("Home Page - Deliveries Link Works", async function () {
  await driver.get("http://localhost:57541/");
  await waitForPageToLoad("jumbotron-order-service")

  await driver
    .findElement(By.id("btn-show-all-deliveries"))
    .click();
  await waitForPageToLoad("deliveries-list-header");
});



test("Delivery Pages - GetDeliveriesList - Loads", async function () {
  await driver.get("http://localhost:57541/Delivery/GetDeliveriesList");
  await waitForPageToLoad("deliveries-list-header")
});

test("Delivery Pages - GetDeliveriesList - Add Link Works", async function () {
  await driver.get("http://localhost:57541/Delivery/GetDeliveriesList");
  await waitForPageToLoad("deliveries-list-header")

  await driver
    .findElement(By.id("btn-add-delivery"))
    .click();
  await waitForPageToLoad("add-delivery-header");
});

test("Delivery Pages - GetDeliveriesList - Update Link Works", async function () {
  await driver.get("http://localhost:57541/Delivery/GetDeliveriesList");
  await waitForPageToLoad("deliveries-list-header")

  await driver
    .findElement(By.id("btn-edit-delivery"))
    .click();
  await waitForPageToLoad("update-delivery-header");
});

test("Delivery Pages - GetDeliveriesList - Delete Link Works", async function () {
  await driver.get("http://localhost:57541/Delivery/GetDeliveriesList");
  await waitForPageToLoad("deliveries-list-header")

  await driver
    .findElement(By.id("btn-delete-delivery"))
    .click();

    //Wait for the alert to be displayed and store it in a variable and dismiss it
    await driver.wait(until.alertIsPresent());
    let alert = await driver.switchTo().alert();
    await alert.dismiss();
});

test("Delivery Pages - AddDelivery - Correct Data - Redirect to GetDeliveriesList", async function () {
  await driver.get("http://localhost:57541/Delivery/AddDelivery");
  await waitForPageToLoad("add-delivery-header")

  await driver
    .findElement(By.id("Name"))
    .sendKeys("New Delivery");
  await driver
    .findElement(By.id("Price"))
    .sendKeys("123");
  await driver
    .findElement(By.id("DeliveryDays"))
    .sendKeys("4");
  await driver
    .findElement(By.id("add-delivery"))
    .click()

  await waitForPageToLoad("deliveries-list-header");
});

test("Delivery Pages - AddDelivery - Incorrect Data (none of 3 fields filled) - Stay on current page and display 3 validation errors", async function () {
  await driver.get("http://localhost:57541/Delivery/AddDelivery");
  await waitForPageToLoad("add-delivery-header")

  await driver
    .findElement(By.id("add-delivery"))
    .click()

  await waitForPageToLoad("add-delivery-header");

  await driver
    .findElements(By.className("field-validation-error")).then( elements => 
      assert.strictEqual(elements.length, 3))
    ;
});

test("Delivery Pages - UpdateDelivery - Incorrect Data (none of 3 fields filled) - Stay on current page and display 3 validation errors", async function () {
  await driver.get("http://localhost:57541/Delivery/UpdateDelivery/1");
  await waitForPageToLoad("update-delivery-header")

  await driver
  .findElement(By.id("Name"))
  .clear()
await driver
  .findElement(By.id("Price"))
  .clear()
await driver
  .findElement(By.id("DeliveryDays"))
  .clear()

  await driver
    .findElement(By.id("update-delivery"))
    .click()

  await waitForPageToLoad("update-delivery-header");

  await driver
    .findElements(By.className("field-validation-error")).then( elements => 
      assert.strictEqual(elements.length, 3))
    ;
});

test("Delivery Pages - UpdateDelivery - Correct Data - Redirect to GetDeliveriesList", async function () {
  await driver.get("http://localhost:57541/Delivery/UpdateDelivery/1");
  await waitForPageToLoad("update-delivery-header")

  await driver
    .findElement(By.id("Name"))
    .clear();
  await driver
    .findElement(By.id("Name"))
    .sendKeys("Delivery1-ChangeByTest");

  await driver
    .findElement(By.id("Price"))
    .clear();
  await driver
    .findElement(By.id("Price"))
    .sendKeys("0.55");

  await driver
    .findElement(By.id("DeliveryDays"))
    .clear();
  await driver
    .findElement(By.id("DeliveryDays"))
    .sendKeys("11");

  await driver
    .findElement(By.id("update-delivery"))
    .click();

  await waitForPageToLoad("deliveries-list-header");
});




test("Product Pages - GetProductList - Loads", async function () {
  await driver.get("http://localhost:57541/Product/GetProductList");
  await waitForPageToLoad("products-list-header")
});

test("Product Pages - GetProductList - Add Link Works", async function () {
  await driver.get("http://localhost:57541/Product/GetProductList");
  await waitForPageToLoad("products-list-header")

  await driver
    .findElement(By.id("btn-add-product"))
    .click();
  await waitForPageToLoad("add-product-header");
});

test("Product Pages - GetProductList - Update Link Works", async function () {
  await driver.get("http://localhost:57541/Product/GetProductList");
  await waitForPageToLoad("products-list-header")

  await driver
    .findElement(By.id("btn-edit-product"))
    .click();
  await waitForPageToLoad("update-product-header");
});

test("Product Pages - GetProductList - Delete Link Works", async function () {
  await driver.get("http://localhost:57541/Product/GetProductList");
  await waitForPageToLoad("products-list-header")

  await driver
    .findElement(By.id("btn-delete-product"))
    .click();

    //Wait for the alert to be displayed and store it in a variable and dismiss it
    await driver.wait(until.alertIsPresent());
    let alert = await driver.switchTo().alert();
    await alert.dismiss();
});

test("Product Pages - AddProduct - Correct Data - Redirect to GetProductList", async function () {
  await driver.get("http://localhost:57541/Product/AddProduct");
  await waitForPageToLoad("add-product-header")

  await driver
    .findElement(By.id("Name"))
    .sendKeys("New Product");
  await driver
    .findElement(By.id("Description"))
    .sendKeys("New Description");
  await driver
    .findElement(By.id("Price"))
    .sendKeys("123");
  await driver
    .findElement(By.id("add-product"))
    .click()

  await waitForPageToLoad("products-list-header");
});

test("Product Pages - AddProduct - Incorrect Data (none of 3 fields filled) - Stay on current page and display 3 validation errors", async function () {
  await driver.get("http://localhost:57541/Product/AddProduct");
  await waitForPageToLoad("add-product-header")

  await driver
    .findElement(By.id("add-product"))
    .click()

  await waitForPageToLoad("add-product-header");

  await driver
    .findElements(By.className("field-validation-error")).then( elements => 
      assert.strictEqual(elements.length, 3))
    ;
});

test("Product Pages - UpdateProduct - Incorrect Data (none of 3 fields filled) - Stay on current page and display 3 validation errors", async function () {
  await driver.get("http://localhost:57541/Product/UpdateProduct/1");
  await waitForPageToLoad("update-product-header")

  await driver
  .findElement(By.id("Name"))
  .clear()
await driver
  .findElement(By.id("Description"))
  .clear()
await driver
  .findElement(By.id("Price"))
  .clear()

  await driver
    .findElement(By.id("update-product"))
    .click()

  await waitForPageToLoad("update-product-header");

  await driver
    .findElements(By.className("field-validation-error")).then( elements => 
      assert.strictEqual(elements.length, 3))
    ;
});

test("Product Pages - UpdateProduct - Correct Data - Redirect to GetProductList", async function () {
  await driver.get("http://localhost:57541/Product/UpdateProduct/1");
  await waitForPageToLoad("update-product-header")

  await driver
    .findElement(By.id("Name"))
    .clear();
  await driver
    .findElement(By.id("Name"))
    .sendKeys("Product1-ChangeByTest");

  await driver
    .findElement(By.id("Description"))
    .clear();
  await driver
    .findElement(By.id("Description"))
    .sendKeys("New-Description");

  await driver
    .findElement(By.id("Price"))
    .clear();
  await driver
    .findElement(By.id("Price"))
    .sendKeys("0.55");

  await driver
    .findElement(By.id("update-product"))
    .click();

  await waitForPageToLoad("products-list-header");
});




test("ClientOrder Pages - GetClientOrderList - Loads", async function () {
  await driver.get("http://localhost:57541/ClientOrder/GetClientOrderList");
  await waitForPageToLoad("client-order-list-header")
});

test("ClientOrder Pages - GetClientOrderList - Add Link Works", async function () {
  await driver.get("http://localhost:57541/ClientOrder/GetClientOrderList");
  await waitForPageToLoad("client-order-list-header")

  await driver
    .findElement(By.id("btn-add-client-order"))
    .click();
  await waitForPageToLoad("add-client-order-header");
});

test("ClientOrder Pages - GetClientOrderList - Update Link Works", async function () {
  await driver.get("http://localhost:57541/ClientOrder/GetClientOrderList");
  await waitForPageToLoad("client-order-list-header")

  await driver
    .findElement(By.id("btn-edit-client-order"))
    .click();
  await waitForPageToLoad("update-client-order-header");
});

test("ClientOrder Pages - GetClientOrderList - Delete Link Works", async function () {
  await driver.get("http://localhost:57541/ClientOrder/GetClientOrderList");
  await waitForPageToLoad("client-order-list-header")

  await driver
    .findElement(By.id("btn-delete-client-order"))
    .click();

  //Wait for the alert to be displayed and store it in a variable and dismiss it
  await driver.wait(until.alertIsPresent());
  let alert = await driver.switchTo().alert();
  await alert.dismiss();
});

test("ClientOrder Pages - AddClientOrder - Correct Data - Redirect to GetProductList", async function () {
  await driver.get("http://localhost:57541/ClientOrder/AddClientOrder");
  await waitForPageToLoad("add-client-order-header")

  await driver
    .findElement(By.css('#ProductId>option[value=\'1\']'))
    .click();
  await driver
    .findElement(By.id("Quantity"))
    .sendKeys("3")
  await driver
    .findElement(By.id("btnAdd"))
    .click()
  await waitForPageToLoad("btnDel");

  await driver
    .findElement(By.css('#ClientId>option[value=\'c983fea1-a550-4ea6-b45a-db66d9defcc1\']'))
    .click();
  await driver
    .findElement(By.css('#DeliveryId>option[value=\'1\']'))
    .click();
  await driver
    .findElement(By.id("Discount"))
    .sendKeys("123");
  await driver
    .findElement(By.id("add-client-order"))
    .click()

  await waitForPageToLoad("client-order-list-header");
});

test("ClientOrder Pages - AddClientOrder - Incorrect Data (none of 3 fields filled) - Stay on current page and display 3 validation errors", async function () {
  await driver.get("http://localhost:57541/ClientOrder/AddClientOrder");
  await waitForPageToLoad("add-client-order-header")

  await driver
    .findElement(By.id("add-client-order"))
    .click()

  await waitForPageToLoad("add-client-order-header");
  await driver.sleep(100);
  await driver
    .findElements(By.className("field-validation-error")).then( elements => 
      assert.strictEqual(elements.length, 3))
    ;
});


test("ClientOrder Pages - UpdateClientOrder - Incorrect Data (none of 2 fields filled) - Stay on current page and display 2 validation errors", async function () {
  await driver.get("http://localhost:57541/ClientOrder/UpdateClientOrder/1");
  await waitForPageToLoad("update-client-order-header")

  await driver
  .findElement(By.css('#ClientId>option[value=\'\']'))
  .click(); // = clear
  await driver
  .findElement(By.css('#DeliveryId>option[value=\'\']'))
  .click(); // = clear

  await driver
    .findElement(By.id("update-client-order"))
    .click()

  await waitForPageToLoad("update-client-order-header");
await driver.sleep(100);
  await driver
    .findElements(By.className("field-validation-error")).then( elements => 
      assert.strictEqual(elements.length, 2))
    ;
});

test("ClientOrder Pages - UpdateClientOrder - Correct Data - Redirect to GetClientOrderList", async function () {
  await driver.get("http://localhost:57541/ClientOrder/UpdateClientOrder/1");
  await waitForPageToLoad("update-client-order-header")

  await driver
    .findElement(By.css('#ClientId>option[value=\'c983fea1-a550-4ea6-b45a-db66d9defcc1\']'))
    .click()
  await driver
    .findElement(By.css('#DeliveryId>option[value=\'2\']'))
    .click();

  await driver
    .findElement(By.id("update-client-order"))
    .click();

  await waitForPageToLoad("client-order-list-header");
});




