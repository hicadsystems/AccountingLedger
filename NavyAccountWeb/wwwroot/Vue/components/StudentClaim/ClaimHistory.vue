<template>
    <!-- WRAPPER -->
    <div>

        <div v-if="errors" class="alert alert-danger alert-dismissible" role="alert">
            <div class="alert-message">
                {{ [errors] }}
            </div>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">�</span>
            </button>
        </div>
        <div v-if="responseMessage" class="alert alert-primary alert-dismissible" role="alert"> <div class="alert-message"> {{ responseMessage }} </div> </div>
        <div class="card-body">


            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Tran Date</th>
                        <th>Term</th>
                        <th>Session</th>
                        <th>Voucher No</th>
                        <th>Claim Amount</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="claim in claimList">
                        <td>{{ claim.transdate }}</td>
                        <td>{{ claim.term }}</td>
                        <td>{{ claim.session }}</td>
                        <td>{{ claim.voucherNumber }}</td>
                        <td>{{ claim.amount }}</td>
                    </tr>
                </tbody>

            </table>

        </div>

    </div>

    <!-- END WRAPPER -->
</template>

<script>
   
export default {
    
    props:['refno','schoolsession','schoolterm'],
    data() {
        return {
        responseMessage:'',
            errors: null,
            searchData: '',
            claimList : null,
        canProcess : true,
        postBody: {
                studentid:0

        }
        };
        },
        mounted() {
            this.$store.state.objectToUpdate = null,
                axios
                    .get(`/api/StudentClaim/getStudentHistoryByRegno/${this.refno.replace('/', '')}/${this.schoolsession}/${this.schoolterm}`)
                    .then(response => (this.claimList = response.data))
     },

    watch:{
        'refno':function (newVal, oldval) {

            axios
            .get(`/api/StudentClaim/getStudentHistoryByRegno/${this.refno}`)
                .then(response => (this.claimList = response.data))
        }
    },
        methods: {

          setValueStudent(result) {
             axios
            .get(`/api/StudentClaim/getStudentHistoryByRegno/${result.value}`)
            .then(response => (this.claimList = response.data))

         }
        }
};
</script>