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
                        <th>Date</th>

                        <th>Document No</th>
                        <th>Remarks</th>

                        <th>Debit</th>
                        <th>Credit</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="personLoan in personApplications">
                        <td>{{ personLoan.dateoftransaction }}</td>

                        <td>{{ personLoan.documentno }}</td>
                        <td>{{ personLoan.remarks }}</td>
                        <td>{{ personLoan.debitAmount }}</td>
                        <td>{{ personLoan.creditAmount }}</td>
                    </tr>
                </tbody>

            </table>

        </div>

    </div>

    <!-- END WRAPPER -->
</template>

<script>
   
export default {
    
    props:['refno','accountcode','svcno'],
    data() {
        return {
        responseMessage:'',
            errors: null,
            searchData: '',
        personApplications : null,
        canProcess : true,
        postBody: {
                PersonID:0,
                FundTypeID : null,

                Amount: 0,
                Status : '',
                VoucherNo : '',
                createdby :'',
                remarks : '',
                svcno:'',
                datecreated : null,
                Tenure : '',
                LoanTypeID:null

        }
        };
        },
        mounted() {
            this.$store.state.objectToUpdate = null,
                axios
                //   this.svcno=this.svcno.replace('/', '');
                //   this.refno=this.refno.replace('/', '');
                //   alert(this.accountcode)
                    .get(`/api/AccountHistory/getAccountHistoryByRefNo/${this.refno.replace('/', '')}/${this.accountcode}/${this.svcno.replace('/', '')}`)
                    .then(response => (this.personApplications = response.data))
            
            console.log(this.personApplications)
     },

    watch:{
        'refno':function (newVal, oldval) {

            axios
            .get(`/api/AccountHistory/getAccountHistoryByRefNo/${this.refno}`)
                .then(response => (this.personApplications = response.data))
             console.log(this.personApplications)
        }
    },
        methods: {


        setValuePersonID(result) {
             axios
            .get(`/api/LoanRegister/getLoanListPerPerson/${result.value}/${this.fundtypeid}`)
            .then(response => (this.personApplications = response.data))

         }
        }
};
</script>