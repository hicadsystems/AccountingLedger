<script type="text/javascript">
    $(document).ready(function () {
        $("#indicator").change(
            function () {
                if ($(this).val() == "Specific Main ledger") {
                    $("#mainacct").show();
                    $("#mainacct").parent().show();
                } else {
                    $("#mainacct").hide();
                    $("#mainacct").parent().hide();
                }
            }
        )
});
</script>