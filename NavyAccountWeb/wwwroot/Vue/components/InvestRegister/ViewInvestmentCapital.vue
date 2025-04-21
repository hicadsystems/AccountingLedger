<template>
    <!-- WRAPPER -->
    <div>

        <div class="card-body">
             <div class="row" style="position:relative;top:-10px;">
                    <div class="col-4">
                        <div class="col-6">
                            <div class="btn-group mr-2 sw-btn-group-extra" role="group">
                              <a class="btn btn-submit btn-primary" :href="'/InvestRegisterMvc/InvestmentCapital'" target="_blank" type="button">{{submitorUpdate2}}</a></div>
                        </div>
                    </div>
                </div>
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Company</th>
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
                        <td>{{ fundrate.issuancebank }}</td>
                        <td>{{ fundrate.voucher }}</td>
                        <td> {{ fundrate.description}}</td>
                        <td>{{ fundrate.amount }}</td>
                        <td> {{ fundrate.date}}</td>
                        <td> {{ fundrate.unit}}</td>



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
                InvestList:null,
                submitorUpdate2:'Print',
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