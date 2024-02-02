<template>
    <!-- WRAPPER -->
    <div>

        <div class="card-body">
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Company</th>
                        <th>Stock Name</th>
                        <th>Registra</th>
                        <th>Voucher</th>
                        <th>Description</th>
                        <th>Amount</th>
                        <th>Date</th>
                        <th>Unit</th>



                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="fundrate in  InvestList">
                        <td>{{ fundrate.company }}</td>
                        <td>{{ fundrate.stockName }}</td>
                        <td>{{ fundrate.issuancebank }}</td>
                        <td>{{ fundrate.voucher }}</td>
                        <td>{{ fundrate.description}}</td>
                        <td>{{ fundrate.amount }}</td>
                        <td>{{ fundrate.date}}</td>
                        <td>{{ fundrate.unit}}</td>
                        <td><button type="button" class="btn btn-submit btn-primary" @click="processRetrieve(fundrate)">Edit</button></td>
                    </tr>
                </tbody>

            </table>
        </div>

    </div>

    <!-- END WRAPPER -->
</template>

<script>
export default {

            data() {
                return {
                InvestList:null
                };
            },
        created() {
            this.$store.state.objectToUpdate = null;
        },

    mounted () {
        axios
            .get('/api/PfInvest/getAllCapital')
            .then(response => (this.InvestList = response.data))
     },
     methods: {

         processRetrieve: function (invest) {

            this.$store.state.objectToUpdate = invest;
        }
    }

};
</script>