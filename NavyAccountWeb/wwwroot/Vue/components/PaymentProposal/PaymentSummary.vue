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
                    <th>School Name</th>
                    <th>Class</th>
                    <th>Session</th>
                    <th>Amount</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="pay in paymentList">
                    <td>{{ pay.schoolName }}</td>
                    <td>{{ pay.className }}</td>
                    <td>{{ pay.session }}</td>
                    <td>{{ pay.amount }}</td>
                    </tr>
                </tbody>

            </table>

        </div>

    </div>

    <!-- END WRAPPER -->
</template>

<script>
   
export default {
    
    props:['refno'],
    data() {
        return {
        responseMessage:'',
            errors: null,
            searchData: '',
            paymentList : null,
        canProcess : true,
       // postBody: {
          //      studentid:0

      //  }
        };
        },
        mounted() {
            this.$store.state.objectToUpdate = null,
                axios
                    .get(`/api/Payment/getPaymentSummary/${this.refno}`)
                    .then(response => (this.paymentList = response.data))
     },

    watch:{
        'refno':function (newVal, oldval) {
            axios
            .get(`/api/Payment/getPaymentSummary/${this.refno}`)
                .then(response => (this.paymentList = response.data))
        }
    },
        
};
</script>