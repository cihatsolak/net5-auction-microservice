const hubAddress = "http://localhost:8001/auctionhub";

var groupName = "Auction-" + $("#AuctionId").val();

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

connection.on("BidsAsync", function (user, bid) {
    addBidToTable(user, bid);
});

$("#send-button").click(function () {

    var sendBidRequestModel = {
        AuctionId: $("#AuctionId").val(),
        ProductId: $("#ProductId").val(),
        SellerUserName: $("#SellerUserName").val(),
        Price: parseFloat($("#price").val()).toString()
    };

    sendBid(sendBidRequestModel);
    event.preventDefault();
});

$("#finish").click(function (event) {
    let auctionId = $("#AuctionId").val();
    sendCompleteBid(auctionId);
    event.preventDefault();
});

function sendBid(sendBidRequestModel) {
    $.ajax({
        url: "/Auction/SendBid",
        type: "POST",
        data: sendBidRequestModel,
        success: function (response) {
            if (response.isSuccess) {
                $("#price").val("");
                connection.invoke("SendBidAsync", groupName, sendBidRequestModel.SellerUserName, sendBidRequestModel.Price)
                    .catch(function (error) {
                        console.log(error);
                    });
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus, errorThrown);
        }
    });
}

function addBidToTable(user, bid) {

    let tbodyString = "<tr>";
    tbodyString += "<td>" + user + "</td>";
    tbodyString += "<td>" + bid + "</td>";
    tbodyString += "</tr>";

    if ($('table > tbody> tr:first').length > 0) {
        $('table > tbody> tr:first').before(tbodyString);
    } else {
        $('.bidLine').append(tbodyString);
    }

}

function sendCompleteBid(auctionId) {
    $.ajax({
        url: "/Auction/CompleteAuction",
        type: "POST",
        data: { auctionId: auctionId },
        success: function (isSuccess) {
            if (isSuccess) {
                console.log("Your transaction was successful!");
                windows.location.href = "/Auction/Index";
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus, errorThrown);
        }
    });
}