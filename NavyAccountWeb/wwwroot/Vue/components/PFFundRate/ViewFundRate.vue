<template>
    <!-- WRAPPER -->
    <div>

        <div class="card-body">
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Code</th>
                        <th>Interest</th>
                        <th>Rate</th>
                        <th>Period</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="fundrate in fundRateList">
                        <td>{{ fundrate.fundCode }}</td>
                        <td>{{ fundrate.interest }}</td>
                        <td>{{ fundrate.rate }}</td>
                        <td> {{ fundrate.period }}</td>
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
                fundRateList:null
                };
            },
        created() {
            this.$store.state.objectToUpdate = null;
        },

    mounted () {
        axios
            .get('/api/PFfundRate/getAllPFundRate')
            .then(response => (this.fundRateList = response.data))
     },
     methods: {

         processRetrieve: function (fundrate) {
             
            this.$store.state.objectToUpdate = fundrate;
        }
    }

};
</script>