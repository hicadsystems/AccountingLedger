<template>
    <!-- WRAPPER -->
    <div>

        <div class="card-body">
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Personnel</th>
                        <th>Application Date</th>

                    </tr>
                </thead>
                <tbody>
                    <tr v-for="LoanType in loantypeList">
                        <td>{{ LoanType.svcno}}</td>
                        <td>{{ LoanType.appdate }}</td>
                       
                        <td><button type="button" class="btn btn-submit btn-primary" @click="processRetrieve(LoanType)">Update</button></td>
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
                loantypeList:null
                };
            },
        created() {
            this.$store.state.objectToUpdate = null;
        },

    mounted () {
        axios
            .get('/api/ClaimRegister/LoadAllClaim')
            .then(response => (this.loantypeList = response.data))
     },
     methods: {

        processRetrieve : function (LoanType) {
            this.$store.state.objectToUpdate = LoanType;
        }
    }

};
</script>