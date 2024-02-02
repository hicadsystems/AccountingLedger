<template>
    <!-- WRAPPER -->
    <div>

        <div class="card-body">
            <div class="row">
            <div class="col-12 col-md-4">
                    <label>Start Date</label>
                    <vuejsDatepicker v-model="startDate" input-class="form-control" name="startDate"></vuejsDatepicker>
                </div>
                <div class="col-12 col-md-4">
                    <label>End Date</label>
                    <vuejsDatepicker v-model="endDate" input-class="form-control" name="endDate"></vuejsDatepicker>
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
import vuejsDatepicker from 'vuejs-datepicker';
export default {
    components: {
            vuejsDatepicker
        },
            data() {
                return {
                InvestList:null,
                startDate:'',
                endDate:'',
                };
            },
        created() {
            this.$store.state.objectToUpdate = null;
        },

    mounted () {
        axios
            .get(`/api/PfInvest/getAllCapital2/${this.startDate}/${this.endDate}`)
            .then(response => (this.InvestList = response.data))
     },
     methods: {

         processRetrieve: function (invest) {

            this.$store.state.objectToUpdate = invest;
        }
    }

};
</script>