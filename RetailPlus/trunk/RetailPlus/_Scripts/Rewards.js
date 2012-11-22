
function ComputeRedeemRewards()
{
    var decCurrentRewardPoints = 0;
    var decRedeemRewardPoints = 0;
    var decNewRewardPoints = 0;

    decCurrentRewardPoints = document.getElementById("ctrlRedeemRewards_txtCurrentRewardPoints").value;
    decRedeemRewardPoints = document.getElementById("ctrlRedeemRewards_txtRedeemRewardPoints").value;
    decNewRewardPoints = decCurrentRewardPoints - decRedeemRewardPoints;

    document.getElementById("ctrlRedeemRewards_txtNewRewardPoints").value = decNewRewardPoints.toFixed(2);
}
