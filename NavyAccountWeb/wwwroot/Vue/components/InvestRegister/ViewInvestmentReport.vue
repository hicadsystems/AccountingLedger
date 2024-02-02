<template>
    <!-- WRAPPER -->
    <div>

        <div class="card-body">
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
          
                        <th>Issuance Bank</th>
                        <th>Voucher</th>
                        <th>Description</th>
                        <th>Amount</th>
                        <th>Date</th>
                        <th>DueDate</th>
                        <th>InvestmentType</th>
                        <th>Matured Amt</th>
                        <th>Matured Date</th>
                        <th>interest</th>
                        <th>Tenure</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="fundrate in  InvestList">
                        <td>{{ fundrate.issuancebank }}</td>
                        <td>{{ fundrate.voucher }}</td>
                        <td> {{ fundrate.description}}</td>
                        <td>{{ fundrate.amount }}</td>
                        <td> {{ fundrate.date}}</td>
                        <td>{{ fundrate.dueDate }}</td>
                        <td> {{ fundrate.investmentType }}</td>
                        <td> {{ fundrate.maturedamt}}</td>
                        <td> {{ fundrate.maturingdate}}</td>
                        <td> {{ fundrate.interest}}</td>
                        <td> {{ fundrate.tenure }}</td>

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
         watch:{
         '$store.state.objectToUpdate':function (newVal, oldVal) {
            this.getAllInvestment();
        }
    },
    mounted () {
        this.getAllInvestment()
     },
     methods: {

         processRetrieve: function (invest) {

            this.$store.state.objectToUpdate = invest;
         },
         getAllInvestment: function () {
             axios
            .get('/api/PfInvest/getAllRegister')
            .then(response => (this.InvestList = response.data))
         }
    }

};
</script>