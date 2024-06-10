# **Asset Manager API**

A simple .NET REST API to display and store information on assets and their prices

## Endpoints
**Assets**
- Retrieve all assets
  - Endpoint: ```GET /assets```
  - Example Response:
  ```
    [
     {
      "id": "f542e7d1-6fd9-426b-97b9-cc2d84c2b0e2",
      "name": "Microsoft Corporation",
      "symbol": "MSFT",
      "isin": "US5949181045"
     },
    {
      "id": "f1efcfde-024c-4bf5-982d-319d90457998",
      "name": "Apple Inc.",
      "symbol": "AAPL",
      "isin": "US0378331005"
     }
    ]
    ```
- Get assets via symbol
  - Endpoint: ```GET /assets/{insert-symbol}```
  - Example Response:
    ```
    {
      "id": "f1efcfde-024c-4bf5-982d-319d90457998",
      "name": "Microsoft Corporation",
      "symbol": "MSFT",
      "isin": "US5949181045"
    }
    ```
- Add asset
  - Endpoint: ```POST /assets```
  - Example Request:
    ```
    {
      "name": "Microsoft Corporation",
      "symbol": "MSFT",
      "isin": "US5949181045"
    }
    ```
  - Example Response:
    ```
    {
      "id": "b6808a98-22c3-40a0-9957-2bbe1f2be532",
      "name": "Microsoft Corporation",
      "symbol": "MSFT",
      "isin": "US5949181045"
    }
    ```
- Update asset
    - Endpoint: ```PUT /assets/{insert-symbol}```
    - Example Request:
    ```
    {
      "name": "Microsoft Corporation",
      "isin": "US5949181045"
    }
    ```
    - Example Response:
    ```
    {
      "id": "b6808a98-22c3-40a0-9957-2bbe1f2be532",
      "name": "Microsoft Corporation",
      "symbol": "MSFT",
      "isin": "US5949181045"
    }
    ```

**Prices**
- Get Prices 
  - Endpoint: ```GET /prices/?symbol=MSFT&date=2024-06-10```
  - Example Response:
  ```
    [
     {
        "id": "4c2cda4e-5903-402e-bafa-4d90385381cf",
        "symbol": "MSFT",
        "date": "2024-06-08T00:00:00",
        "value": 20,
        "source": "Thompson Reuters",
        "timeStamp": "2024-06-10T13:10:51.9810758Z"
     },
     {
        "id": "d2834bfa-2d3a-4a80-bd58-5fb4c2d713ce",
        "symbol": "MSFT",
        "date": "2024-06-08T00:00:00",
        "value": 488.69,
        "source": "LSEG",
        "timeStamp": "2024-06-10T14:14:18.1248868+01:00"
     }
    ]
    ```
- Add prices to asset
  - Endpoint: ```POST /prices```
  - Example Request:
    ```
    {
     "symbol": "NVDA",
     "date": "2024-06-10",
     "value": 111.08,
     "source": "Thompson Reuters"
    }
    ```
  - Example Response:
    ```
    {
     "id": "2dd5a7ca-dc69-49f4-a4f8-e0e227696ea9",
     "symbol": "NVDA",
     "date": "2024-06-10T00:00:00",
     "value": 111.08,
     "source": "Thompson Reuters",
     "timeStamp": "2024-06-10T13:12:19.7546602Z"
    }
    ```
- Update price
  - Endpoint: ```PUT /prices```
  - Example Request:
    ```
    {
     "id": "2dd5a7ca-dc69-49f4-a4f8-e0e227696ea9",
     "symbol": "NVDA",
     "date": "2024-06-10T00:00:00",
     "value": 111.08,
     "source": "Thompson Reuters"
    }
    ```
  - Example Response:
    ```
    {
     "id": "2dd5a7ca-dc69-49f4-a4f8-e0e227696ea9",
     "symbol": "NVDA",
     "date": "2024-06-10T00:00:00",
     "value": 111.08,
     "source": "Thompson Reuters",
     "timeStamp": "2024-06-10T13:12:19.7546602Z"
    }
    ```

## Design Choices
- This solution was implemented with the repository pattern, where due to time constraints, I decided to settle on a Dictionary to store the data in-memory
- This also included a base repository which covered the common repository actions (Get, Add & Update) for both Assets and Prices
- SOLID principles covered:
  - Separate/single responsibility between assets and prices
  - Derived repository classes can be substituted for the base repository class, without affecting the correctness of the application
  - Interfaces used, where they have been segregated by their responsibilities, in order to reduce coupling between classes
  - Dependency injection used for the asset and price services into the controller, also allowing for reduced coupling & dependencies

## Assumptions
- Symbol used as the identifier for assets, and what would be the link between Assets and prices
- Updates would update the entire entity, instead of a partial update 

## Drawbacks
- Error handling can be improved on (maybe the use of a global error handler or some exception middleware)
- No implementation of an actual data storage in the DAL
