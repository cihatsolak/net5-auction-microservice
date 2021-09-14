const hubAddress = "https://localhost:5003/auctionhub";

var groupName = "Auction-" + $("AuctionId").val();

var connection = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Debug)
    .withAutomaticReconnect(1000, 2000, 4000, 6000)
    .withUrl(hubAddress).build();

connection.start()
    .then(() => {
        $("#send-button").prop('disabled', false);

        connection.invoke("AddToGroupAsync", groupName)
            .catch(function (error) {
                console.log(error);
            });
    })
    .catch((error) => {
        console.log(error);
        $("#send-button").prop('disabled', true);
    });

$("#send-button").click(function () {

    var sendBidRequestModel = {
        AuctionId: $("AuctionId").val(),
        ProductId: $("#ProductId").val(),
        SellerUserName: $("#SellerUserName").val(),
        Price: parseFloat($("#Price").val()).toString()
    };

    sendBid(sendBidRequestModel);
    event.preventDefault();
});

function sendBid(sendBidRequestModel) {

}